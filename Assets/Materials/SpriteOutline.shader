Shader "Unlit/SpriteOutline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    // Add values to determine if outlining is enabled and outline color.
        _OutlineWidth("Outline Width", Float) = 1
        _Outline("Outline", Float) = 0
        _OutlineColor("Outline Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Outline;
            float _OutlineWidth;
            fixed4 _OutlineColor;
            float4 _MainTex_TexelSize;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                // If outline is enabled and there is a pixel, try to draw an outline.
                if (_Outline > 0 && col.a != 0) {
                    // Get the neighbouring four pixels.
                    fixed4 pixelUp = tex2D(_MainTex, i.uv + fixed2(0, _MainTex_TexelSize.y * _OutlineWidth));
                    fixed4 pixelDown = tex2D(_MainTex, i.uv - fixed2(0, _MainTex_TexelSize.y * _OutlineWidth));
                    fixed4 pixelRight = tex2D(_MainTex, i.uv + fixed2(_MainTex_TexelSize.x * _OutlineWidth, 0));
                    fixed4 pixelLeft = tex2D(_MainTex, i.uv - fixed2(_MainTex_TexelSize.x * _OutlineWidth, 0));
                    col.a = 0;

                    // If one of the neighbouring pixels is invisible, we render an outline.
                    if (pixelUp.a * pixelDown.a * pixelRight.a * pixelLeft.a == 0) {
                        col.rgba = fixed4(1, 1, 1, 1) * _OutlineColor;
                    }
                }

                return col;
            }
            ENDCG
        }
    }
}
