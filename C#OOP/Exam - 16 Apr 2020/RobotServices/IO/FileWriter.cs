
namespace RobotService.IO
{
    using RobotService.IO.Contracts;
    using System.IO;
    using System;

    class FileWriter : IWriter
    {
        public void Write(string message)
        {
            throw new System.NotImplementedException();
        }

        public void WriteLine(string message)
        {
            File.AppendAllText("../../../output.txt", message + Environment.NewLine);
        }
    }
}
