Shader "UI/ColorSpecificTransparency"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TargetTransparentColor ("Target Transparent Color", Color) = (1, 1, 1, 1) // 要变透明的颜色
        _TargetFixedColor ("Target Fixed Color", Color) = (0, 0, 0, 1)            // 要保持不变的颜色
        _Threshold ("Threshold", Range(0, 1)) = 0.1                              // 颜色匹配的阈值
        _TransparencyStrength ("Transparency Strength", Range(0, 1)) = 1.0       // 透明强度
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
                // 采样当前像素的颜色
                fixed4 col = tex2D(_MainTex, i.uv);

                // 计算与目标透明颜色的差异
                float transparentDiff = distance(col.rgb, _TargetTransparentColor.rgb);

                // 计算与目标固定颜色的差异
                float fixedDiff = distance(col.rgb, _TargetFixedColor.rgb);

                // 如果颜色接近固定颜色，保持不透明
                if (fixedDiff < _Threshold)
                {
                    col.a = 1.0; // 完全不透明
                }
                else
                {
                    // 如果接近透明颜色，则根据透明度调整透明度
                    float transparency = saturate((transparentDiff - _Threshold) / _Threshold);
                    col.a *= lerp(1.0, transparency, _TransparencyStrength);
                }

                // 将原始图像的 alpha 值与计算出来的 alpha 值取较低的那个
                col.a = min(col.a, tex2D(_MainTex, i.uv).a);

                return col;
            }
            ENDCG
        }
    }
}
