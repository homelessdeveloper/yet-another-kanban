import type { Meta, StoryObj } from '@storybook/react';
import { Button } from '.';

// ----------------------------------------------------------- //
// STORY META
// ----------------------------------------------------------- //

const meta: Meta<typeof Button> = {
  title: 'Shared/Buttons/Button',
  component: Button,
  tags: ["autodocs"],
};

export default meta;

// ----------------------------------------------------------- //
// STORIES
// ----------------------------------------------------------- //

type Story = StoryObj<typeof Button>;


export const Default: Story = {
  render: (args) => <Button {...args} />,
  args: {
    children: 'Button',
    variant: 'primary'
  }
}


