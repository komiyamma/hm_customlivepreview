﻿// ★秀丸クラス
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public sealed partial class HmCustomLivePreviewDynamicLib
{
    public sealed partial class Hidemaru
    {
        public sealed class ExplorerPane
        {
            static ExplorerPane()
            {
                SetUnManagedDll();
            }


            // モードの設定
            public static int SetMode(int mode)
            {
                try
                {
                    int result = pExplorerPane_SetMode(Hidemaru.WindowHandle, (IntPtr)mode);
                    return result;
                }
                catch (Exception e)
                {
                    OutputDebugStream(ErrorMsg.MethodNeedExplorerOperation + ":\n" + e.Message);
                }

                return 0;
            }

            // モードの取得
            public static int GetMode()
            {
                try
                {
                    int result = pExplorerPane_GetMode(Hidemaru.WindowHandle);
                    return result;
                }
                catch (Exception e)
                {
                    OutputDebugStream(ErrorMsg.MethodNeedExplorerOperation + ":\n" + e.Message);
                }

                return 0;
            }

            // LoadProjectする
            public static int LoadProject(string filepath)
            {
                try
                {
                    byte[] encode_data = HmOriginalEncodeFunc.EncodeWStringToOriginalEncodeVector(filepath);
                    int result = pExplorerPane_LoadProject(Hidemaru.WindowHandle, encode_data);
                    return result;
                }
                catch (Exception e)
                {
                    OutputDebugStream(ErrorMsg.MethodNeedExplorerOperation + ":\n" + e.Message);
                }

                return 0;
            }

            // SaveProjectする
            public static int SaveProject(string filepath)
            {
                try
                {
                    byte[] encode_data = HmOriginalEncodeFunc.EncodeWStringToOriginalEncodeVector(filepath);
                    int result = pExplorerPane_SaveProject(Hidemaru.WindowHandle, encode_data);
                    return result;
                }
                catch (Exception e)
                {
                    OutputDebugStream(ErrorMsg.MethodNeedExplorerOperation + ":\n" + e.Message);
                }

                return 0;
            }

            // GetProjectする
            public static string GetProject()
            {
                try
                {
                    IntPtr startpointer = pExplorerPane_GetProject(Hidemaru.WindowHandle);
                    List<byte> blist = GetPointerToByteArray(startpointer);

                    string project_name = Hidemaru.HmOriginalDecodeFunc.DecodeOriginalEncodeVector(blist);

                    if (String.IsNullOrEmpty(project_name))
                    {
                        return null;
                    }
                    return project_name;

                    /*
                    if (hmJSDynamicLib.Hidemaru.Macro.IsExecuting)
                    {
                        string cmd = @"dllfuncstr(loaddll(""HmExplorerPane""), ""GetProject"", hidemaruhandle(0))";
                        string project_name = (string)hmJSDynamicLib.Hidemaru.Macro.Var(cmd);
                        if (String.IsNullOrEmpty(project_name))
                        {
                            return null;
                        }
                        return project_name;
                    }
                    else
                    {
                        string cmd = @"endmacro dllfuncstr(loaddll(""HmExplorerPane""), ""GetProject"", hidemaruhandle(0))";
                        var result = hmJSDynamicLib.Hidemaru.Macro.ExecEval(cmd);
                        string project_name = result.Message;
                        if (String.IsNullOrEmpty(project_name))
                        {
                            return null;
                        }
                        return result.Message;
                    }
                    */
                }
                catch (Exception e)
                {
                    OutputDebugStream(ErrorMsg.MethodNeedExplorerOperation + ":\n" + e.Message);
                }

                return null;
            }

            private static List<byte> GetPointerToByteArray(IntPtr startpointer)
            {
                List<byte> blist = new List<byte>();

                int index = 0;
                while (true)
                {
                    var b = Marshal.ReadByte(startpointer, index);

                    blist.Add(b);

                    // 文字列の終端はやはり0
                    if (b == 0)
                    {
                        break;
                    }

                    index++;
                }

                return blist;
            }

            // GetCurrentDirする
            public static string GetCurrentDir()
            {
                if (version < 885)
                {
                    OutputDebugStream(ErrorMsg.MethodNeedExplorerNotFound + ":\n" + "GetCurrentDir");
                    return "";
                }
                try
                {
                    IntPtr startpointer = pExplorerPane_GetCurrentDir(Hidemaru.WindowHandle);
                    List<byte> blist = GetPointerToByteArray(startpointer);

                    string currentdir_name = Hidemaru.HmOriginalDecodeFunc.DecodeOriginalEncodeVector(blist);

                    if (String.IsNullOrEmpty(currentdir_name))
                    {
                        return null;
                    }
                    return currentdir_name;

                    /*
                    if (hmJSDynamicLib.Hidemaru.pExplorerPane_GetCurrentDir != null)
                    {
                        if (hmJSDynamicLib.Hidemaru.Macro.IsExecuting)
                        {
                            string cmd = @"dllfuncstr(loaddll(""HmExplorerPane""), ""GetCurrentDir"", hidemaruhandle(0))";
                            string currentdir_name = (string)hmJSDynamicLib.Hidemaru.Macro.Var(cmd);
                            if (String.IsNullOrEmpty(currentdir_name))
                            {
                                return null;
                            }
                            return currentdir_name;
                        }
                        else
                        {
                            string cmd = @"endmacro dllfuncstr(loaddll(""HmExplorerPane""), ""GetCurrentDir"", hidemaruhandle(0))";
                            var result = hmJSDynamicLib.Hidemaru.Macro.ExecEval(cmd);
                            string currentdir_name = result.Message;
                            if (String.IsNullOrEmpty(currentdir_name))
                            {
                                return null;
                            }
                            return result.Message;
                        }
                    }
                    */
                }
                catch (Exception e)
                {
                    OutputDebugStream(ErrorMsg.MethodNeedExplorerOperation + ":\n" + e.Message);
                }

                return null;
            }

            // GetUpdated
            public static int GetUpdated()
            {
                try
                {
                    int result = pExplorerPane_GetUpdated(Hidemaru.WindowHandle);
                    return result;
                }
                catch (Exception e)
                {
                    OutputDebugStream(ErrorMsg.MethodNeedExplorerOperation + ":\n" + e.Message);
                }

                return 0;
            }

            public static IntPtr WindowHandle
            {
                get
                {
                    return pExplorerPane_GetWindowHandle(Hidemaru.WindowHandle);
                }
            }

            public static IntPtr SendMessage(int commandID)
            {
                //
                // loaddll "HmExplorerPane.dll";
                // #h=dllfunc("GetWindowHandle",hidemaruhandle(0));
                // #ret=sendmessage(#h,0x111/*WM_COMMAND*/,251,0); //251=１つ上のフォルダ
                //
                return HmCustomLivePreviewDynamicLib.SendMessage(ExplorerPane.WindowHandle, 0x111, commandID, IntPtr.Zero);
            }

        }
    }
}

