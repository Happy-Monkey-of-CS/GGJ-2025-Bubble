Shader "UI/ColorSpecificTransparency"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TargetTransparentColor ("Target Transparent Color", Color) = (1, 1, 1, 1) // Ҫ��͸������ɫ
        _TargetFixedColor ("Target Fixed Color", Color) = (0, 0, 0, 1)            // Ҫ���ֲ������ɫ
        _Threshold ("Threshold", Range(0, 1)) = 0.1                              // ��ɫƥ�����ֵ
        _TransparencyStrength ("Transparency Strength", Range(0, 1)) = 1.0       // ͸��ǿ��
    }
    SubShader
    {
        Tags { "Queue"="Overlay" "RenderType"="Transparent" "IgnoreProjector"="True" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off
        Lighting Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _TargetTransparentColor;
            float4 _TargetFixedColor;
            float _Threshold;
            float _TransparencyStrength;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // ������ǰ���ص���ɫ
                fixed4 col = tex2D(_MainTex, i.uv);

                // ������Ŀ��͸����ɫ�Ĳ���
                float transparentDiff = distance(col.rgb, _TargetTransparentColor.rgb);

                // ������Ŀ��̶���ɫ�Ĳ���
                float fixedDiff = distance(col.rgb, _TargetFixedColor.rgb);

                // �����ɫ�ӽ��̶���ɫ�����ֲ�͸��
                if (fixedDiff < _Threshold)
                {
                    col.a = 1.0; // ��ȫ��͸��
                }
                else
                {
                    // ����ӽ�͸����ɫ�������͸���ȵ���͸����
                    float transparency = saturate((transparentDiff - _Threshold) / _Threshold);
                    col.a *= lerp(1.0, transparency, _TransparencyStrength);
                }

                // ��ԭʼͼ��� alpha ֵ���������� alpha ֵȡ�ϵ͵��Ǹ�
                col.a = min(col.a, tex2D(_MainTex, i.uv).a);

                return col;
            }
            ENDCG
        }
    }
}
