import { Sidebar } from "@yak/web/modules/sidebar/widgets";
import { FC, PropsWithChildren } from "react";
import { Scrollbars } from 'react-custom-scrollbars-2';
import { useWindowSize } from "react-use"


/**
 * The main layout of the application
 *
 * @since 0.0.1
 */
export const DefaultLayout: FC<PropsWithChildren> = ({ children }) => {
  const { height } = useWindowSize();

  return (
    <div className="min-h-screen flex flex-col flex-auto flex-shrink-0 antialiased bg-gray-50 text-gray-800">
      <Sidebar />
      <main className="ml-64 p-4 min-h-screen">
        <Scrollbars
          autoHeight
          autoHeightMin={height - 50} autoHeightMax={height - 50}
        >
          {children}
        </Scrollbars>
      </main>
    </div >
  )
};
