Shader "Custom/CircleTransition"
{
    Properties
    {
        _Radius ("Radius", Range(0,1)) = 1
        _Softness ("Softness", Range(0,0.2)) = 0.01
        _Color ("Color", Color) = (0,0,0,1)
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

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

            float _Radius;
            float _Softness;
            fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

                fixed4 frag (v2f i) : SV_Target
            {
                float2 center = float2(0.5, 0.5);

                float2 uv = i.uv;

                float aspect = _ScreenParams.x / _ScreenParams.y;
                uv.x = (uv.x - 0.5) * aspect + 0.5;

                float dist = distance(uv, float2(0.5, 0.5));

                if(dist < _Radius)
                    return fixed4(0,0,0,0);

                return fixed4(0,0,0,1);
            }
            ENDCG
        }
    }
}
