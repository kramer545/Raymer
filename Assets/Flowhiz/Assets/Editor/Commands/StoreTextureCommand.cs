using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlowMapEditor
{
    public class StoreTextureCommand : UndoableCommand
    {
        private Texture2D paintingTexture;
        private Texture2D storedTexture;

        public StoreTextureCommand(Texture2D paintingTexture)
        {
            this.paintingTexture = paintingTexture;
        }

        public override void Execute()
        {
            storedTexture = new Texture2D(paintingTexture.width, paintingTexture.height);
            storedTexture.SetPixels(paintingTexture.GetPixels());
            storedTexture.Apply();
        }

        public override void Undo()
        {
            paintingTexture.SetPixels(storedTexture.GetPixels());
            paintingTexture.Apply();
        }
    }
}
