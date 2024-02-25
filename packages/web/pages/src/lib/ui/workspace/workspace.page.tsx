import { FC } from "react";
import { useParams } from "react-router-dom";
import { DefaultLayout, ProtectedLayout } from "../../layouts";
import { MdOutlineEditNote } from "react-icons/md"
import { WorkspaceContextProvider, WorkspaceModel } from "@yak/web/modules/workspace/entities";
import { KanbanView } from "@yak/web/modules/workspace/widgets";
import { ROUTES } from "../../routes";
import { RenameWorkspaceModal } from "@yak/web/modules/workspace/features";

/**
 * Page of the single workspace.
 * This component forms a root of the workpsace context which
 * operations are mostly similar to the "Aggregate Root" from the domain driven design.
 *
 * @since 0.0.1
 */
export const WorkspacePage: FC = () => {
  const { workspaceId } = useParams<{ workspaceId: string }>();
  const getWorkspaceById = WorkspaceModel.useGetWorkspaceById(workspaceId ?? "", {
    enabled: !!workspaceId
  });

  const workspace = getWorkspaceById.data;

  if (!workspaceId) return <h1> Error: WorkspaceID is not defiend </h1>
  if (!workspace) return <h1>Lodaing...</h1>

  return (
    <ProtectedLayout redirectTo={ROUTES.Auth.SignIn.getPath()}>
      <DefaultLayout>
        <WorkspaceContextProvider workspaceId={workspaceId} >
          <div className="flex flex-col w-full h-full text-gray-700">
            <div className="px-10 mt-6 flex items-center">
              <h1 className="text-2xl font-bold mr-1.5">{workspace.name}</h1>
              <button
                onClick={() => RenameWorkspaceModal.show({})}
                className="text-2xl w-8 h-8 rounded-lg border-2 border-indigo-400 flex items-center justify-center bg-indigo-100"
              >
                <MdOutlineEditNote />
              </button>
            </div>
            <div>
              <KanbanView />
            </div>
          </div>
        </WorkspaceContextProvider>
      </DefaultLayout>
    </ProtectedLayout>
  )
}
