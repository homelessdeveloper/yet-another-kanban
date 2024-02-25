import z from "zod";
import log from "loglevel";
import { useId } from 'react';
import { useForm } from 'react-hook-form';
import { toast } from 'react-hot-toast';
import { zodResolver } from "@hookform/resolvers/zod"
import { Button, TextInput, Modal } from '@yak/web/shared/ui';
import { MODULE_NAME } from "../constants";
import {
  AssignmentTitle,
  WorkspaceModalManager,
  WorkspaceModel,
} from '@yak/web/modules/workspace/entities';

/**
 * Schema definition for the data required to create a new assignment.
 *
 * @since 0.0.1
 */
export type CreateAssignmentFormData = z.infer<typeof CreateAssignmentFormData>
export const CreateAssignmentFormData = z.object({
  title: AssignmentTitle,
  description: z.string()
});

/**
 * Props for the CreateAssignmentModal component.
 *
 * @since 0.0.1
 */
export type CreateWorkspaceModalProps = {
  groupId: string;
};

/**
 * Modal component for creating a new assignment.
 *
 * @since 0.0.1
 */
export const CreateAssignmentModal =
  WorkspaceModalManager.register<CreateWorkspaceModalProps>(
    `${MODULE_NAME}/create-assignment-modal`,
    (props) => {
      const { groupId } = props;
      const formId = useId();
      const modal = WorkspaceModalManager.useModal();
      const createAssignment = WorkspaceModel.useCreateAssignment();

      const {
        register,
        handleSubmit,
        reset,
        formState: { errors },
      } = useForm<CreateAssignmentFormData>({
        mode: "all",
        resolver: zodResolver(CreateAssignmentFormData)
      });

      const onClose = () => {
        reset();
        log.debug("[<CreateAssignmentModal/>]: Modal has been reseted")
        modal.hide();
      }

      const onSubmit = async (request: CreateAssignmentFormData) => {
        await toast.promise(
          createAssignment.mutateAsync({
          // TODO: something is fucked up here
            groupId,
            request: {
              groupId,
              ...request
            }
          }),
          {
            loading: 'Creating assignment...',
            success: 'Assignment created',
            error: 'Oops, something bad happened',
          }
        );

        onClose();
      };

      return (
        <Modal onClose={onClose} show={modal.isVisible}>
          <Modal.Panel>
            {/* Heading */}
            <Modal.Header>
              <Modal.Title>Add assignment</Modal.Title>
              <Modal.CloseButton>Close Modal</Modal.CloseButton>
            </Modal.Header>

            {/* Description */}
            <Modal.Body>
              <form id={formId} onSubmit={handleSubmit(onSubmit)}>
                <TextInput
                  className="w-full"
                  label="Title"
                  error={errors.title !== undefined}
                  required
                  {...register('title', { required: true })}
                />
                {errors.title && (
                  <p className="text-red-600">{errors.title.message}</p>
                )}

                <TextInput
                  className="w-full"
                  label="Description"
                  error={errors.title !== undefined}
                  required
                  {...register('description')}
                />
                {errors.description && (
                  <p className="text-red-600">{errors.description.message}</p>
                )}
              </form>
            </Modal.Body>

            {/* Footer */}
            <Modal.Footer>
              <Button type="submit" form={formId} children={'Accept'} />
              <Button
                onClick={onClose}
                variant="secondary"
                children={'Decline'}
              />
            </Modal.Footer>
          </Modal.Panel>
        </Modal>
      );
    }
  );
