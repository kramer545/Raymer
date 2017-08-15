using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlowMapEditor
{
    public class ClearCommand : UndoableCommand
    {
        private Texture2D paintingTexture;
        private Texture2D storedTexture;
        private Color clearColor;

        public ClearCommand(Texture2D paintingTexture)
        {
            this.paintingTexture = paintingTexture;
            clearColor = new Color(0.5f, 0.5f, 1.0f, 1.0f);
        }

        public override void Execute()
        {
            storedTexture = new Texture2D(paintingTexture.width, paintingTexture.height);
            storedTexture.SetPixels(paintingTexture.GetPixels());
            storedTexture.Apply();

            for (int i = 0; i < paintingTexture.width; i++)
            {
                for (int j = 0; j < paintingTexture.height; j++)
                {
                    paintingTexture.SetPixel(i, j, clearColor);
                }
            }

            paintingTexture.Apply();
        }

        public override void Undo()
        {
            paintingTexture.SetPixels(storedTexture.GetPixels());
            paintingTexture.Apply();
        }
    }
}
