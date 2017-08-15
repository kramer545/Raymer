using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlowMapEditor
{
    public class Brush
    {
        public enum DrawType
        {
            FlowMap,
            Alpha
        }

        private Vector2 currentUVPosition;
        public Vector2 CurrentUVPosition
        {
            get
            {
                return currentUVPosition;
            }

        }
        public Vector2 PreviousUVPosition { get; private set; }
        public Vector2 CurrentWorldPosition { get; private set; }
        public Vector2 PreviousWorldPosition { get; private set; }

        public float Size { get; set; }
        public float AngleToSmooth { get; set; }
        public Vector2 Direction { get { return Vector3.Normalize(PreviousWorldPosition - CurrentWorldPosition); } }
        public Vector2 SmoothedDirection
        {
            get
            {
                float smoothAngle = Vector2.Angle(Direction, PreviousDirection);

                if (smoothAngle > AngleToSmooth)
                {
                    return Vector2.Lerp(PreviousDirection, Direction, AngleToSmooth / smoothAngle);
                }

                return Vector3.Normalize(PreviousUVPosition - CurrentUVPosition);
            }
        }


        public Vector2 PreviousDirection { get; private set; }
        public float Softness { get; set; }
        public float AlphaIntensivity { get; set; }
        public DrawType drawType { get; set; }

        public Brush()
        {
            Size = 0.2f;
            AngleToSmooth = 45;
            Softness = 1.0f;
            AlphaIntensivity = 0.5f;
        }

        public bool IsCanDrawing(float textureTexel)
        {
            float brushOffset = Vector2.Distance(CurrentUVPosition, PreviousUVPosition);

            return brushOffset > 20.0f * textureTexel * Mathf.Max(Size, 0.1f)
                && Mathf.Abs(CurrentUVPosition.x * CurrentUVPosition.y) <= 1 
                && Mathf.Abs(PreviousUVPosition.x * PreviousUVPosition.y) <= 1;
                }

        public void UpdateCurrentUVPosition(Vector2 currentUVPosition, Vector2 currentWorldPosition, float texel)
        {
            if (IsCanDrawing(texel))
            {
                PreviousDirection = Direction;
                PreviousUVPosition = this.currentUVPosition;
                PreviousWorldPosition = CurrentWorldPosition;
            }
            this.currentUVPosition = currentUVPosition;
            this.CurrentWorldPosition = currentWorldPosition;
        }
    }
}
