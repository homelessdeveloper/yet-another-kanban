import z from "zod";
import log from "loglevel";
import { toast } from 'react-hot-toast';
import { useForm } from "react-hook-form";
import { useId } from "react";
import { WorkspaceModel, WorkspaceModalManager, WorkspaceName } from "@yak/web/modules/workspace/entities"
import { Button, TextInput, Modal } from "@yak/web/shared/ui";
import { zodResolver } from "@hookform/resolvers/zod";
import { MODULE_NAME } from "../constants"

/**
 * Data schema for renaming a workspace.
 *
 * @since 0.0.1
 */
export type RenameWorkspaceFormData = z.infer<typeof RenameWorkspaceFormData>
export const RenameWorkspaceFormData = z.object({ name: WorkspaceName });

/**
 * Modal component for renaming a workspace.
 *
 * @since 0.0.1
 */
export const RenameWorkspaceModal = WorkspaceModalManager.register(`${MODULE_NAME}/rename-workspace-modal`, () => {
  const modal = WorkspaceModalManager.useModal();
  const formId = useId();
  const renameWorkspace = WorkspaceModel.useRenameWorkspace();
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<RenameWorkspaceFormData>({ resolver: zodResolver(RenameWorkspaceFormData) });

  const onClose = () => {
    reset();
    log.debug("[<RenameWorkspaceModal/>]: Modal has been reseted")
    modal.hide();
  };

  const onSubmit = async (data: RenameWorkspaceFormData) => {
    await toast.promise(
      renameWorkspace.mutateAsync(data),
      {
        error: "Oops, something went wrong!",
        loading: "Renaming workspace...",
        success: "Workspace renamed",
      }
    );

    onClose();
  }

  return (
    <Modal show={modal.isVisible} onClose={onClose} >
      <Modal.Panel>
        <Modal.Header>
          <Modal.Title as="h2">Rename workspace</Modal.Title>
          <Modal.CloseButton>Close Modal</Modal.CloseButton>
        </Modal.Header>
        <Modal.Body>
          <form
            id={formId}
            onSubmit={handleSubmit(onSubmit)}>
            <TextInput
              className="w-full"
              label="Workspace name"
              error={errors.name !== undefined}
              required
              {...register("name")}
            />
            {errors.name && <p className="text-red-600">{errors.name.message}</p>}
          </form>
        </Modal.Body>
        <Modal.Footer>
          <Button
            type="submit"
            form={formId}
            children={"Accept"}
          />
          <Button
            onClick={onClose}
            variant="secondary"
            children={"Decline"}
          />
        </Modal.Footer>
      </Modal.Panel>
    </Modal >
  )
});
