using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlowMapEditor
{
    public class GenerateTextureCommand : UndoableCommand
    {
        private Texture2D previousTexture;
        private Texture2D currentTexture;
        private int size;
        private Color color;

        public GenerateTextureCommand(Texture2D currentTexture, int size, Color color)
        {
            this.size = size;
            this.currentTexture = currentTexture;
            this.color = color;
        }

        public override void Execute()
        {
            if (currentTexture)
            {
                previousTexture = new Texture2D(currentTexture.width, currentTexture.height);
                previousTexture.SetPixels(currentTexture.GetPixels());
                previousTexture.Apply();
            }
            else
            {
                previousTexture = new Texture2D(256, 256);
                currentTexture = new Texture2D(size, size);
            }

            currentTexture.Resize(size, size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    currentTexture.SetPixel(i, j, color);
                }
            }
            currentTexture.Apply();
        }

        public override void Undo()
        {
            currentTexture = new Texture2D(previousTexture.width, previousTexture.height);
            currentTexture.SetPixels(previousTexture.GetPixels());
            currentTexture.Apply();
        }
    }
}
