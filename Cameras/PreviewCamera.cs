using Assets.Scripts.Core;
using Assets.Scripts.Extensions;
using Assets.Scripts.Interaction;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Cameras {
    public class PreviewCamera : BaseComponent {

        protected override void Awake() {
            base.Awake();
            Camera = GetComponent<Camera>();
            Camera.transparencySortMode = TransparencySortMode.Perspective;
            Camera.clearFlags = CameraClearFlags.SolidColor;
            Camera.backgroundColor = new Color(1, 1, 1, 0);
        }

        protected void Update() {
            if (Target != null) {
                //Target.transform.RotateAround(Target.GetComponent<Renderer>().bounds.center, new Vector3(0, 1, 0), 1);

                //Target.transform.Rotate(Vector3.right * Time.deltaTime * RotationSpeed);
                Target.transform.Rotate(Vector3.up * Time.deltaTime * RotationSpeed);
            }
        }

        public void Clear() {
            Target = null;
            Camera.targetTexture = null;
        }

        public RenderTexture Preview(GameObject target, RawImage image) {
            if (target == Target) {
                return null;
            }

            if (Target != null) {
                Destroy(Target);
            }

            Target = target;

            if (Target != null) {
                Target.Remove<Rigidbody>();
                Target.Remove<Collider>();
                Target.Remove<ApplyForce>();

                Target.With<MeshRenderer>(meshRenderer => {
                    meshRenderer.material.shader = PreviewShader;
                    meshRenderer.material.SetFloat("_Outline", PreviewOutline);
                });

                var texture = image.texture as RenderTexture;

                if (texture == null) {
                    texture = new RenderTexture(256, 256, 16);
                    image.texture = texture;
                    image.material = PreviewMaterial;
                }

                Camera.enabled = true;
                Camera.targetTexture = texture;

                Target.transform.parent = transform;
                Target.transform.localPosition = new Vector3(0, 0, PreviewDistance);

                return texture;
            }

            Camera.enabled = false;
            Camera.targetTexture = null;
            return null;
        }

        public GameObject Target;

        public Shader PreviewShader;

        public Material PreviewMaterial;

        public float PreviewOutline = 0.05f;

        public float PreviewDistance = 1f;

        public float RotationSpeed = 40;
        
        private Camera Camera { get; set; }
    }
}