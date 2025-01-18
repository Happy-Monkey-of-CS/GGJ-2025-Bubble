Shader "Custom/SpriteWhiteToTransparent"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _TransparencyStrength ("Transparency Strength", Range(0, 1)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            Name "SpriteWhiteToTransparent"
            Tags { "LightMode"="UniversalForward" }

            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // Properties
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float _TransparencyStrength;

            struct Attributes
            {
                float4 positionOS : POSITION; // 顶点坐标
                float2 uv : TEXCOORD0;       // 纹理坐标
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION; // 裁剪空间坐标
                float2 uv : TEXCOORD0;           // 纹理坐标
            };

            Varyings vert(Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS);
                o.uv = v.uv;
                return o;
            }

            float4 frag(Varyings i) : SV_Target
            {
                // 获取纹理颜色
                float4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);

                // 计算亮度
                //float brightness = dot(texColor.rgb, float3(0.299, 0.587, 0.114));

                // 根据亮度调整 Alpha 值（白色越多越透明）
                //texColor.a *= lerp(1.0, 0.0, brightness * _TransparencyStrength);

                return texColor;
            }
            ENDHLSL
        }
    }
}
