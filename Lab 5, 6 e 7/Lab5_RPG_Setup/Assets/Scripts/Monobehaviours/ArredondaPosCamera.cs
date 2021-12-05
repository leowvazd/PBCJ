using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ArredondaPosCamera : CinemachineExtension
{
    public float PixelsPerUnit = 32;                    // armazena a quantidade de pixel por unidade
    protected override void /// <summary>
    /// Configuração da camera em referencia ao player
    /// </summary>
    PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            Vector3 pos = state.FinalPosition;
            Vector3 pos2 = new Vector3 (Round(pos.x), Round(pos.y), pos.z);
            state.PositionCorrection += pos2 - pos;
        }
    }

    float 
    /// <summary>
    /// Float auxiliar para calculo de configuração de câmera
    /// </summary>
    Round (float x)
    {
        return Mathf.Round(x * PixelsPerUnit ) / PixelsPerUnit;
    }
}