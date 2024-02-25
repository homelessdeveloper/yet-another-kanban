import type { Meta, StoryObj } from '@storybook/react';
import { TextInput } from '.';

// ----------------------------------------------------------- //
// STORY META
// ----------------------------------------------------------- //

const meta: Meta<typeof TextInput> = {
  component: TextInput,
  title: 'Shared/Inputs/TextInput',
} as Meta<typeof TextInput>;

export default meta;

// ----------------------------------------------------------- //
// STORIES
// ----------------------------------------------------------- //

type Story = StoryObj<typeof TextInput>;


export const Default: Story = {
  render: (args) => <TextInput {...args} />,
  args: {
    color: "pink",
  }
}


