import { FC } from "react";
import { cn } from "@yak/libs/cn"
import { CreateWorkspaceModal } from "@yak/web/modules/workspace/features"
import { BiTrashAlt } from "react-icons/bi";
import { Link } from "react-router-dom";
import { toast } from "react-hot-toast";
import { Scrollbars } from "react-custom-scrollbars-2"
import { WorkspaceModel } from "@yak/web/modules/workspace/entities";

/**
 * Primary sidebar of the application.
 *
 * @since 0.0.1
 */
export const Sidebar: FC = () => {
  const listWorkspaces = WorkspaceModel.useListWorkspaces();
  const deleteWorkspace = WorkspaceModel.useDeleteWorkspace({
    onMutate(workspaceId) {
      toast.loading("Deleting workspace...", { id: workspaceId })
    },

    onSuccess(_, workspaceId) {
      toast.success("Workspace deleted!", { id: workspaceId })
    },

    onError(_, workspaceId) {
      toast.error("Oops, something went wrong! Workspace was not deleted", { id: workspaceId })
    }
  });

  return (
    <aside className="fixed flex flex-col top-0 left-0 w-64 bg-white h-full border-r">
      <div className="flex items-center  justify-center h-14 border-b mb-4">
        <div>YetAnotherKanban</div>
      </div>
      <div className="flex flex-col">
        {/* <Worksapces> */}
        <div className="flex flex-col max-h-[250px]">
          <div className="pl-5 ">
            <div className="text-sm font-light tracking-wide text-gray-500 h-8">
              Workspaces
            </div>
          </div>

          <div className="flex flex-col">
            <div className="overflow-hidden">
              <Scrollbars
                autoHeight
                autoHeightMin={0}
                autoHeightMax={450}
              >
                {listWorkspaces.data && listWorkspaces.data.map(workspace => (
                  <li
                    key={workspace.id}
                    className={cn(
                      'relative flex items-center h-11 flex justify-between',
                      'focus:outline-none hover:bg-gray-50 text-gray-600',
                      'hover:text-gray-800 border-l-4 border-transparent',
                      'hover:border-indigo-500 px-5',
                      'overflow-x-hidden',
                      location.pathname === `/${workspace.id}` && 'bg-gray-50 text-gray-600 border-indigo-500'
                    )}
                  >
                    <Link
                      to={`/${workspace.id}`}
                      className="flex items-center"
                    >
                      <div className="w-5 h-5 bg-indigo-600 rounded-full" />
                      <span className="ml-2 text-sm tracking-wide truncate font-medium w-[150px]">
                        {workspace.name}
                      </span>
                    </Link>
                    <div className="flex items-center">
                      <button onClick={() => deleteWorkspace.mutate(workspace.id)}>
                        <BiTrashAlt />
                      </button>
                    </div>
                  </li>
                ))}

              </Scrollbars>
            </div>

            <div
              className={cn(
                'relative flex  items-center h-11',
                'opacity-75 hover:opacity-100',
                'focus:outline-none hover:bg-gray-50 text-gray-600',
                'hover:text-gray-800 border-l-4 border-transparent',
                'hover:border-pink-500 px-5'
              )}
            >
              <button
                onClick={() => CreateWorkspaceModal.show({})}
                className="flex items-center"
              >
                <div
                  className="w-5 h-5 rounded-full border-2  border-gray-600 flex items-center justify-center"
                  children="+"
                />
                <span className="ml-2 text-sm tracking-wide truncate font-medium">
                  Add new
                </span>
              </button>
            </div>
          </div>
        </div>
        {/* </Workspace> */}
      </div>
    </aside >
  );
}
