using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 本脚本挂到摄像机上
/// </summary>
public class WaveDiffusion_Postprocessing : MonoBehaviour
{
    public RenderTexture _rt;
    public Shader waveDiffusionShader;
    public Vector4 waveCenter = Vector4.zero;         //扩散中心
    public Color waveColor = Color.red;               //波纹颜色
    public float waveSpeed = 1;                       //扩散速度
    public float waveInterval = 5;                    //扩散间隔
    public float wavePower = 10;                      //波纹强度
    public float waveColorPower = 10;                 //波纹颜色强度

    private Material waveDiffusionMat;                //渲染材质  
    private Matrix4x4 vpMatrix4x4_inverse;            //VP逆矩阵

    private void Start()
    {
        //设置相机获取深度
        Camera.main.depthTextureMode = DepthTextureMode.Depth;
        waveDiffusionMat = new Material(waveDiffusionShader);
        //计算VP逆矩阵
        Matrix4x4 vpMatrix4x4_inverse = Camera.main.projectionMatrix * Camera.main.worldToCameraMatrix;
        vpMatrix4x4_inverse = vpMatrix4x4_inverse.inverse;
        //设置矩阵
        waveDiffusionMat.SetMatrix("_VPMatrix4x4_inverse", vpMatrix4x4_inverse);
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        //设置渲染材质参数
        waveDiffusionMat.SetVector("_WaveCenter", waveCenter);
        waveDiffusionMat.SetFloat("_WaveSpeed", waveSpeed);
        waveDiffusionMat.SetFloat("_WaveInterval", waveInterval);
        waveDiffusionMat.SetFloat("_WavePower", wavePower);
        waveDiffusionMat.SetFloat("_WaveColorPower", waveColorPower);
        waveDiffusionMat.SetColor("_WaveColor", waveColor);
        //渲染
        Graphics.Blit(src, dst, waveDiffusionMat);
        
    }
 
}
