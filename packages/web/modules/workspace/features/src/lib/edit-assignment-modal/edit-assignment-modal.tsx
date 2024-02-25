import z from "zod";
import toast from "react-hot-toast";
import log from "loglevel";
import { useId } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { AssignmentTitle, WorkspaceModalManager, WorkspaceModel } from "@yak/web/modules/workspace/entities";
import { MODULE_NAME } from "../constants";
import {
  Button,
  TextInput,
  Modal,
} from "@yak/web/shared/ui";

/**
 * Schema definition for the data required to edit an assignment.
 *
 * @since 0.0.1
 */
export type EditAssignmentModalData = z.infer<typeof EditAssignmentModalData>;
export const EditAssignmentModalData = z.object({
  title: AssignmentTitle.optional(),
  description: z.string().optional()
});


/**
  * Props for the EditAssignmentModal component.
  *
  * @since 0.0.1
  */
export type EditAssignmentModalProps = { assignmentId: string; }

/**
 * Modal component for editing an assignment.
 *
 * @since 0.0.1
 */
export const EditAssignmentModal = WorkspaceModalManager.register<EditAssignmentModalProps>(`${MODULE_NAME}/edit-assignment-modal`, (props) => {
  const modal = WorkspaceModalManager.useModal();
  const formId = useId();
  const updateAssignment = WorkspaceModel.useUpdateAssignment();

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors }
  } = useForm<EditAssignmentModalData>({
    mode: "all",
    resolver: zodResolver(EditAssignmentModalData)
  });

  const onClose = () => {
    reset();
    log.debug("[<UpdateAssignmentModal />]: workspace modal has been cleared.")
    modal.hide();
  }

  const onSubmit = async (data: EditAssignmentModalData) => {
    await toast.promise(
      updateAssignment.mutateAsync({
        ...data,
        assignmentId: props.assignmentId
      }),
      {
        loading: "Saving changes...",
        error: "Oops! Something went wrong",
        success: "Changes saved"
      }
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
          <Modal.Title>Edit assignment</Modal.Title>
          <Modal.CloseButton> Close Modal </Modal.CloseButton>
        </Modal.Header>

        {/* Description */}
        <Modal.Body>
          <form
            id={formId}
            onSubmit={handleSubmit(onSubmit)}
          >
            <TextInput
              className="w-full"
              label="Title"
              error={errors.title !== undefined}
              required
              {...register("title", { required: true })}
            />
            {errors.title && <p className="text-red-600">{errors.title.message}</p>}

            <TextInput
              className="w-full"
              label="Description"
              error={errors.title !== undefined}
              required
              {...register("description")}
            />
            {errors.description && <p className="text-red-600">{errors.description.message}</p>}
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
  );
});
