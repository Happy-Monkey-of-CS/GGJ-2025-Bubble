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
                float4 positionOS : POSITION; // ��������
                float2 uv : TEXCOORD0;       // ��������
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION; // �ü��ռ�����
                float2 uv : TEXCOORD0;           // ��������
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
                // ��ȡ������ɫ
                float4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);

                // ��������
                //float brightness = dot(texColor.rgb, float3(0.299, 0.587, 0.114));

                // �������ȵ��� Alpha ֵ����ɫԽ��Խ͸����
                //texColor.a *= lerp(1.0, 0.0, brightness * _TransparencyStrength);

                return texColor;
            }
            ENDHLSL
        }
    }
}
