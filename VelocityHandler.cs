﻿using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using WhoIsTalking;
using System.Globalization;

namespace WhoIsSpeeding
{
    internal class VelocityHandler : MonoBehaviour
    {
        public NameTagHandler nameTagHandler;
        private string velocity;
        private float lastTime = 0;
        private float cooldown = 0.5f;
        public GameObject fpTag, tpTag, firstPersonNameTag, thirdPersonNameTag;
        public Renderer fpTextRenderer, fpVelocityRenderer, tpVelocityRenderer;
        public TextMeshPro fpVelocityText, tpVelocityText;
        public Shader uiShader = Shader.Find("UI/Default");

        void Start()
        {
            if (firstPersonNameTag == null || thirdPersonNameTag == null)
            {
                CreateVelocityTags();
            }
        }

        public void CreateVelocityTags()
        {
            if (firstPersonNameTag == null)
            {
                Transform tmpchild0 = transform.FindChildRecursive("First Person NameTag");
                firstPersonNameTag = tmpchild0.FindChildRecursive("NameTag").gameObject;

                fpTag = GameObject.CreatePrimitive(PrimitiveType.Quad);
                fpTag.name = "FP Velocity Holder";
                fpTag.transform.SetParent(firstPersonNameTag.transform);
                fpTag.transform.localPosition = new Vector3(0f, 2f, 0f);
                fpTag.transform.localScale = Vector3.one;
                fpTag.layer = firstPersonNameTag.layer;

                Destroy(fpTag.GetComponent<Collider>());

                fpVelocityRenderer = fpTag.GetComponent<Renderer>();
                fpVelocityRenderer.material = new Material(uiShader);

                fpVelocityText = fpTag.AddComponent<TextMeshPro>();
                fpVelocityText.alignment = TextAlignmentOptions.Center;
                fpVelocityText.transform.rotation = Quaternion.Euler(0, 180, 0);
                fpVelocityText.font = nameTagHandler.rig.playerText1.font;
                fpVelocityText.fontSize = 7;
                fpVelocityText.text = "0.0";
                fpVelocityText.color = Color.green;
            }

            if (thirdPersonNameTag == null)
            {
                Transform tmpchild1 = transform.FindChildRecursive("Third Person NameTag");
                thirdPersonNameTag = tmpchild1.FindChildRecursive("NameTag").gameObject;

                tpTag = GameObject.CreatePrimitive(PrimitiveType.Quad);
                tpTag.name = "TP Velocity Holder";
                tpTag.transform.SetParent(thirdPersonNameTag.transform);
                tpTag.transform.localPosition = new Vector3(0f, 2f, 0f);
                tpTag.transform.localScale = Vector3.one;
                tpTag.layer = thirdPersonNameTag.layer;

                Destroy(tpTag.GetComponent<Collider>());

                tpVelocityRenderer = tpTag.GetComponent<Renderer>();
                tpVelocityRenderer.material = new Material(uiShader);

                tpVelocityText = tpTag.AddComponent<TextMeshPro>();
                tpVelocityText.alignment = TextAlignmentOptions.Center;
                tpVelocityText.transform.rotation = Quaternion.Euler(0, 180, 0);
                tpVelocityText.font = nameTagHandler.rig.playerText1.font;
                tpVelocityText.fontSize = 7;
                tpVelocityText.text = "0.0";
                tpVelocityText.color = Color.green;
            }
            UpdateVelocityPatchThingy();
        }

        void UpdateVelocityPatchThingy()
        {
            if (fpVelocityText != null)
            {
                fpVelocityText.text = "0.0";
            }

            if (tpVelocityText != null)
            {
                tpVelocityText.text = "0.0";
            }
        }

        void FixedUpdate()
        {
            if (nameTagHandler != null)
            {
                lastTime += Time.deltaTime;

                if (lastTime >= cooldown)
                {
                    Vector3 velocityVector = nameTagHandler.rig.LatestVelocity();
                    float speedMagnitude = velocityVector.magnitude;
                    velocity = speedMagnitude.ToString("F1", CultureInfo.InvariantCulture);
                    lastTime = 0f;

                    if (tpVelocityText != null)
                    {
                        tpVelocityText.text = velocity;
                    }

                    if (fpVelocityText != null)
                    {
                        fpVelocityText.text = velocity;
                    }

                    if (speedMagnitude <= 6.5f)
                    {
                        if (fpVelocityText != null)
                        {
                            fpVelocityText.color = Color.green;
                        }
                        if (tpVelocityText != null)
                        {
                            tpVelocityText.color = Color.green;
                        }
                    }
                    else if (speedMagnitude > 6.5f && speedMagnitude < 7.0f)
                    {
                        if (fpVelocityText != null)
                        {
                            fpVelocityText.color = Color.yellow;
                        }
                        if (tpVelocityText != null)
                        {
                            tpVelocityText.color = Color.yellow;
                        }
                    }
                    else if (speedMagnitude >= 7.0f)
                    {
                        if (fpVelocityText != null)
                        {
                            fpVelocityText.color = Color.red;
                        }
                        if (tpVelocityText != null)
                        {
                            tpVelocityText.color = Color.red;
                        }
                    }
                }

                if (fpTextRenderer == null)
                {
                    fpTextRenderer = fpTag.transform.parent.GetComponent<Renderer>();
                }
                if (fpVelocityRenderer == null)
                {
                    fpVelocityRenderer = fpVelocityText.GetComponent<Renderer>();
                }
                else
                {
                    fpVelocityRenderer.forceRenderingOff = fpTextRenderer.forceRenderingOff;
                }
            }
        }
    }
}