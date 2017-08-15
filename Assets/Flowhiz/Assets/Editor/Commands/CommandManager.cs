using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlowMapEditor
{
    public class CommandManager
    {
        private List<Command> commandList;
        private int maxOperationCount;

        public CommandManager(int maxOperationCount)
        {
            this.maxOperationCount = maxOperationCount;
            commandList = new List<Command>(maxOperationCount);
        }

        public void ExecuteCommand(Command cmd)
        {
            cmd.Execute();
            if (cmd is UndoableCommand)
            {
                commandList.Add(cmd);
                while (commandList.Count >= maxOperationCount)
                {
                    commandList.RemoveAt(0);
                }
            }
        }

        public void Undo()
        {
            if (commandList.Count > 0)
            {
                UndoableCommand cmd = (UndoableCommand)commandList[commandList.Count - 1];
                cmd.Undo();
                commandList.RemoveAt(commandList.Count - 1);
            }
        }
    }
}
