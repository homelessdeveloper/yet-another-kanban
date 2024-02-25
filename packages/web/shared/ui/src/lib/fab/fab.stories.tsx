import { Fab } from '.';
import type { Meta, StoryObj } from '@storybook/react';
import { AiOutlineFolderAdd } from 'react-icons/ai';

// ----------------------------------------------------------- //
// STORY META
// ----------------------------------------------------------- //

const meta: Meta<typeof Fab> = {
  title: "Shared/Fab",
  component: Fab,
} as Meta<typeof Fab>;

export default meta;

// ----------------------------------------------------------- //
// STORIES
// ----------------------------------------------------------- //

type Story = StoryObj<typeof Fab>;

export const Default: Story = {
  render: () => (
    <div
      className="bg-blue-50"
      style={{
        width: '100%',
        height: '100vh',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
      }}
      children={
        <Fab>
          <Fab.Item children={<AiOutlineFolderAdd />} />
          <Fab.Item children={<AiOutlineFolderAdd />} />
        </Fab>
      }
    />
  ),
};
