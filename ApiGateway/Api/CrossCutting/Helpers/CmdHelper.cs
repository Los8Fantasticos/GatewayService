using System.Diagnostics;

namespace Api.CrossCutting.Helpers
{
    public class CmdHelper
    {
        public static void RunCommand(string cmd)
        {
            try
            {
                var processInfo = new ProcessStartInfo("docker", $""+cmd);

                processInfo.CreateNoWindow = true;
                processInfo.UseShellExecute = false;
                processInfo.RedirectStandardOutput = true;
                processInfo.RedirectStandardError = true;

                int exitCode;
                using (var process = new Process())
                {
                    process.StartInfo = processInfo;

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    process.WaitForExit(1200000);
                    if (!process.HasExited)
                    {
                        process.Kill();
                    }

                    exitCode = process.ExitCode;
                    process.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string RunCommandOutput(string cmd)
        {
            string Output = string.Empty;
            try
            {
                var processInfo = new ProcessStartInfo("docker", $"" + cmd);

                processInfo.CreateNoWindow = true;
                processInfo.UseShellExecute = false;
                //processInfo.RedirectStandardOutput = true;
                processInfo.RedirectStandardError = true;

                int exitCode;
                using (var process = new Process())
                {
                    process.StartInfo = processInfo;
                    process.Start();
                    StreamReader reader = process.StandardOutput;
                    Output = reader.ReadToEnd();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    process.WaitForExit(1200000);
                    if (!process.HasExited)
                    {
                        process.Kill();
                    }
                    exitCode = process.ExitCode;
                    process.Close();
                }
                return Output;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
