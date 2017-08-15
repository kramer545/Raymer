using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlowMapEditor
{
    public abstract class UndoableCommand : Command
    {
        public abstract void Undo();
    }
}
