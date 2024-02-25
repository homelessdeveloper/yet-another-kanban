import log from "loglevel";
import z from 'zod';
import { MODULE_NAME } from "../constants";
import { useId } from "react";
import { useForm } from "react-hook-form";
import { WorkspaceModalManager, WorkspaceModel, GroupName } from "@yak/web/modules/workspace/entities";
import { zodResolver } from "@hookform/resolvers/zod";
import { toast } from "react-hot-toast";
import {
  Button,
  TextInput,
  Modal,
} from "@yak/web/shared/ui";

/**
 * Schema definition for the data required to rename a group.
 *
 * @since 0.0.1
 */
export type RenameGroupModalData = z.infer<typeof RenameGroupModalData>;
export const RenameGroupModalData = z.object({
  name: GroupName
});

/**
 * Props for the RenameGroupModal component.
 *
 * @since 0.0.1
 */
export type RenameGroupModalProps = { groupId: string }

/**
 * Modal component for renaming a group.
 *
 * @since 0.0.1
 */
export const RenameGroupModal = WorkspaceModalManager.register<RenameGroupModalProps>(`${MODULE_NAME}/rename-group-modal`, (props) => {
  const { groupId } = props;
  const modal = WorkspaceModalManager.useModal();
  const formId = useId();

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors }
  } = useForm<RenameGroupModalData>({
    mode: "all",
    resolver: zodResolver(RenameGroupModalData)
  });

  const renameGroup = WorkspaceModel.useRenameGroup();

  const onClose = () => {
    reset();
    log.debug("[<RenameGroupModal/>]: Modal has been reseted")
    modal.hide();
  };

  const onSubmit = async (data: RenameGroupModalData) => {
    await toast.promise(
      renameGroup.mutateAsync({
        groupId,
        ...data
      }),
      {
        loading: "Renaming the group...",
        success: "Group renamed",
        error: "Oops, something bad happened",
      }
    )

    onClose();
  };

  return (
    <Modal
      onClose={onClose}
      show={modal.isVisible}
    >
      <Modal.Panel>
        {/* Heading */}
        <Modal.Header>
          <Modal.Title>Rename group</Modal.Title>
          <Modal.CloseButton> Close Modal </Modal.CloseButton>
        </Modal.Header>

        {/* Description */}
        <Modal.Body>
          <form
            id={formId}
            onSubmit={handleSubmit(onSubmit)}>
            <TextInput
              className="w-full"
              label="Group name"
              error={errors.name !== undefined}
              required
              {...register("name")}
            />
            {errors.name && <p className="text-red-600">{errors.name.message}</p>}
          </form>
        </Modal.Body>

        {/* Footer */}
        <Modal.Footer>
          <Button
            type="submit"
            form="edit-group-modal-form"
            children={"Accept"}
          />
          <Button
            onClick={onClose}
            variant="secondary"
            children={"Decline"}
          />
        </Modal.Footer>
      </Modal.Panel>
    </Modal>
  )
});
