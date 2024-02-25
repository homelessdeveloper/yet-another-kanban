import type { Meta, StoryObj } from '@storybook/react';
import { TextPill } from '.';

// ----------------------------------------------------------- //
// STORY META
// ----------------------------------------------------------- //

const meta: Meta<typeof TextPill> = {
  component: TextPill,
  title: 'Shared/Pills/TextPill',
} as Meta<typeof TextPill>;

export default meta;

// ----------------------------------------------------------- //
// STORIES
// ----------------------------------------------------------- //

type Story = StoryObj<typeof TextPill>;


export const Default: Story = {
  render: (args) => <TextPill {...args} />,
  args: {
    color: "pink",
    text: "Design"
  }
}


