Shader "Unlit/TargetColorTransparency"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TargetColor ("Target Color", Color) = (1, 1, 1, 1) // 默认白色
        _Threshold ("Threshold", Range(0, 1)) = 0.1          // 差异范围
        _TransparencyStrength ("Transparency Strength", Range(0, 1)) = 1.0 // 透明强度
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
                // 采样当前像素的颜色
                fixed4 col = tex2D(_MainTex, i.uv);

                // 计算与目标颜色的差异（欧几里得距离）
                float colorDiff = distance(col.rgb, _TargetColor.rgb);

                // 将差异映射到透明度范围
                float transparency = saturate((colorDiff - _Threshold) / _Threshold);

                // 调整透明度强度
                col.a *= lerp(1.0, transparency, _TransparencyStrength);

                // 应用雾效
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
