Shader "Unlit/TargetColorTransparency"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TargetColor ("Target Color", Color) = (1, 1, 1, 1) // Ĭ�ϰ�ɫ
        _Threshold ("Threshold", Range(0, 1)) = 0.1          // ���췶Χ
        _TransparencyStrength ("Transparency Strength", Range(0, 1)) = 1.0 // ͸��ǿ��
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _TargetColor;
            float _Threshold;
            float _TransparencyStrength;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // ������ǰ���ص���ɫ
                fixed4 col = tex2D(_MainTex, i.uv);

                // ������Ŀ����ɫ�Ĳ��죨ŷ����þ��룩
                float colorDiff = distance(col.rgb, _TargetColor.rgb);

                // ������ӳ�䵽͸���ȷ�Χ
                float transparency = saturate((colorDiff - _Threshold) / _Threshold);

                // ����͸����ǿ��
                col.a *= lerp(1.0, transparency, _TransparencyStrength);

                // Ӧ����Ч
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
