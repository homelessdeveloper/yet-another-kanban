import React, { useState } from 'react';
import { Modal } from '.';
import type { Meta, StoryObj } from '@storybook/react';
import { constVoid } from '@effect/data/Function';
import { Button, PrimaryButton } from '../buttons';
import { useToggle } from "react-use";

// ----------------------------------------------------------- //
// STORY META
// ----------------------------------------------------------- //

const meta: Meta<typeof Modal> = {
  component: Modal,
  title: 'Shared/Modal',
} as Meta<typeof Modal>;

export default meta;

// ----------------------------------------------------------- //
// STORIES
// ----------------------------------------------------------- //

type Story = StoryObj<typeof Modal>;

export const Default: Story = {
  render: (args) => {
    const [isModalOpen, toggleModal] = useToggle(false);

    // const sample = (

    //             <p className="text-base leading-relaxed text-gray-500 dark:text-gray-400">
    //               With less than a month to go before the European Union enacts new consumer privacy laws for its citizens, companies around the world are updating their terms of service agreements to comply.
    //             </p>
    //             <p className="text-base leading-relaxed text-gray-500 dark:text-gray-400">
    //               The European Union’s General Data Protection Regulation (G.D.P.R.) goes into effect on May 25 and is meant to ensure a common set of data rights in the European Union. It requires organizations to notify users as soon as possible of high-risk data breaches that could personally affect them.
    //             </p>
    // )

    return (
      <div>
        <Modal
          onClose={toggleModal}
          show={isModalOpen}
        >
          <Modal.Panel>
            {/* Heading */}
            <Modal.Header>
              <Modal.Title>This is title</Modal.Title>
              <Modal.CloseButton> Close Modal </Modal.CloseButton>
            </Modal.Header>

            {/* Description */}
            <Modal.Body>
              <p className="text-base leading-relaxed text-gray-500 dark:text-gray-400">
                With less than a month to go before the European Union enacts new consumer privacy laws for its citizens, companies around the world are updating their terms of service agreements to comply.
              </p>
              <p className="text-base leading-relaxed text-gray-500 dark:text-gray-400">
                The European Union’s General Data Protection Regulation (G.D.P.R.) goes into effect on May 25 and is meant to ensure a common set of data rights in the European Union. It requires organizations to notify users as soon as possible of high-risk data breaches that could personally affect them.
              </p>
            </Modal.Body>

            {/* Footer */}
            <Modal.Footer>
              <PrimaryButton onClick={toggleModal}>Accept</PrimaryButton>
              <Button onClick={toggleModal}>Decline</Button>
            </Modal.Footer>
          </Modal.Panel>
        </Modal>

        <PrimaryButton onClick={toggleModal}>
          Open Modal
        </PrimaryButton>
      </div>
    );
  }

};
