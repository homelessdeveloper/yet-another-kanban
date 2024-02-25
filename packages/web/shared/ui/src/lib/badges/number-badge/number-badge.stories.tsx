import type { Meta, StoryObj } from '@storybook/react';
import { NumberBadge } from '.';

// ----------------------------------------------------------- //
// STORY META
// ----------------------------------------------------------- //

const meta: Meta<typeof NumberBadge> = {
  title: 'Shared/Badges/NumberBadge',
  component: NumberBadge,
} as Meta<typeof NumberBadge>;

export default meta;

// ----------------------------------------------------------- //
// STORIES
// ----------------------------------------------------------- //

type Story = StoryObj<typeof NumberBadge>;


export const Default: Story = {
  render: (args) => <NumberBadge {...args} />,
  args: {
    number: 12,
  }
}


