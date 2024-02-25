import { MdOutlineEditNote } from "react-icons/md"
import { BiTrashAlt } from "react-icons/bi";

import { ContextMenu, } from '.';
import type { Meta, StoryObj } from '@storybook/react';

// ----------------------------------------------------------- //
// STORY META
// ----------------------------------------------------------- //

const meta: Meta<typeof ContextMenu> = {
  title: "Shared/ContextMenu",
  component: ContextMenu,
} as Meta<typeof ContextMenu>;

export default meta;

// ----------------------------------------------------------- //
// STORIES
// ----------------------------------------------------------- //

type Story = StoryObj<typeof ContextMenu>;

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
        <ContextMenu>
          <ContextMenu.Button>
            Open
          </ContextMenu.Button>

          <ContextMenu.Items>
            <ContextMenu.Item >
              Gray-colored menu item
            </ContextMenu.Item>

            <ContextMenu.Item color="green">
              Green-colored menu item
            </ContextMenu.Item>

            <ContextMenu.Item color="yellow">
              Yellow-colored menu item
            </ContextMenu.Item>

            <ContextMenu.Item color="blue">
              Blue-colored menu item
            </ContextMenu.Item>

            <ContextMenu.Item color="indigo">
              Indigo-colored menu item
            </ContextMenu.Item>

            <ContextMenu.Item color="pink">
              Pink-colored menu item
            </ContextMenu.Item>

            <ContextMenu.Item color="pink">
              Red-colored menu item
            </ContextMenu.Item>

            <ContextMenu.Divider />

            <ContextMenu.Item>
              <MdOutlineEditNote className="text-xl" />
              <span>View</span>
            </ContextMenu.Item>

            <ContextMenu.Item>
              <MdOutlineEditNote className="text-xl" />
              <p>Edit entity</p>
            </ContextMenu.Item>

            <ContextMenu.Divider />

            <ContextMenu.Item >
              <BiTrashAlt />
              <p>Delete entities</p>
            </ContextMenu.Item>

          </ContextMenu.Items>
        </ContextMenu>
      }
    />
  ),
};
