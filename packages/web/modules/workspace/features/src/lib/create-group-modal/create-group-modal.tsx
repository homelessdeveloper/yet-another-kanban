import log from "loglevel";
import z from "zod";
import { useId } from "react";
import { useForm } from "react-hook-form";
import { toast } from "react-hot-toast";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  WorkspaceModalManager,
  WorkspaceModel,
  GroupName
} from "@yak/web/modules/workspace/entities";
import {
  Button,
  TextInput,
  Modal,
} from "@yak/web/shared/ui";
import { MODULE_NAME } from "../constants";

/**
 * Schema definition for the data required to create a new group.
 *
 * @since 0.0.1
 */
export type CreateGroupModalData = z.infer<typeof CreateGroupModalData>;
export const CreateGroupModalData = z.object({
  name: GroupName
});

/**
 * Modal component for creating a new group.
 *
 * @since 0.0.1
 */
export const CreateGroupModal = WorkspaceModalManager.register(`${MODULE_NAME}/create-group-modal`, () => {
  const formId = useId();
  const modal = WorkspaceModalManager.useModal();
  const createGroup = WorkspaceModel.useCreateGroup();
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<CreateGroupModalData>({
    mode: "all",
    resolver: zodResolver(CreateGroupModalData)
  });

  const onClose = () => {
    reset();
    log.debug("[<CreateGroupModal/>]: Modal has been reseted")
    modal.hide();
  }

  const onSubmit = async (data: CreateGroupModalData) => {
    await toast.promise(
      createGroup.mutateAsync(data),
      {
        loading: "Creating group...",
        success: "Group created",
        error: "Oops, something bad happened",
      },
    )

    onClose();
  }

  return (
    <Modal
      onClose={onClose}
      show={modal.isVisible}
    >
      <Modal.Panel>
        {/* Heading */}
        <Modal.Header>
          <Modal.Title>Add new group</Modal.Title>
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
    </Modal>
  )
})
