import * as API from "@yak/web/shared/api";
import log from "loglevel";
import z from "zod"
import { useId } from "react";
import { useForm } from "react-hook-form";
import { useQueryClient } from "@tanstack/react-query";
import { toast } from "react-hot-toast";
import { WorkspaceModel, WorkspaceName } from "@yak/web/modules/workspace/entities";
import { GlobalModalManager } from "@yak/web/shared/modal-manager";
import { zodResolver } from "@hookform/resolvers/zod"

import {
  Button,
  TextInput,
  Modal,
} from "@yak/web/shared/ui";
import { MODULE_NAME } from "../constants";


/**
 * Schema definition for the data required to create a new workspace.
 *
 * @since 0.0.1
 */
export type CreateWorkspaceModalData = z.infer<typeof CreateWorkspaceModalData>
export const CreateWorkspaceModalData = z.object({
  name: WorkspaceName
})

/**
 * Modal component for creating a new workspace.
 *
 * @since 0.0.1
 */
export const CreateWorkspaceModal = GlobalModalManager.register(`${MODULE_NAME}/create-workspace-modal`, ({ }) => {
  const formId = useId();

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors }
  } = useForm<CreateWorkspaceModalData>({
    mode: "all",
    resolver: zodResolver(CreateWorkspaceModalData)
  });

  const modal = GlobalModalManager.useModal();
  const queryClient = useQueryClient();
  const createWorkspace = WorkspaceModel.useCreateWorkspace();


  const onClose = () => {
    reset();
    log.debug("[<CreateWorkspaceModal/>]: workspace modal has been cleared.")
    modal.hide();
  }

  const onSubmit = async (data: CreateWorkspaceModalData) => {
    await toast.promise(
      createWorkspace.mutateAsync(data),
      {
        loading: "Creating new workspace...",
        error: "Oops! Something went wrong",
        success: "Workspace created"
      }
    );
    queryClient.invalidateQueries({ queryKey: WorkspaceModel.getUseListWorkspacesQueryKey(), });

    onClose();
  };

  console.log(errors);

  return (
    <Modal
      onClose={onClose}
      show={modal.isVisible}
    >
      <Modal.Panel>
        <Modal.Header>
          <Modal.Title as="h2">Create new workspace</Modal.Title>
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
            {errors.name && <p className="text-red-600">{errors.name.message?.toString()}</p>}
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
