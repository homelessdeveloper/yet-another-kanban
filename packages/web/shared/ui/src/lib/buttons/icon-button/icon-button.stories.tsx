import { AiOutlinePlus } from "react-icons/ai";
import type { Meta, StoryObj } from '@storybook/react';
import { IconButton } from '.';

// ----------------------------------------------------------- //
// STORY META
// ----------------------------------------------------------- //

const meta: Meta<typeof IconButton> = {
  title: 'Shared/Buttons/IconButton',
  component: IconButton,
} as Meta<typeof IconButton>;

export default meta;

// ----------------------------------------------------------- //
// STORIES
// ----------------------------------------------------------- //

type Story = StoryObj<typeof IconButton>;


export const Default: Story = {
  render: (args) => <IconButton {...args} />,
  args: {
    icon: <AiOutlinePlus />,
  }
}


