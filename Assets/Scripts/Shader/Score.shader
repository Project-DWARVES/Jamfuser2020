Shader "Custom/Score"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0

            #include "UnityCG.cginc"

            uniform fixed4 _Colours[4];
            uniform float _Percentages[4];

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
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                int index = 0;

                fixed4 c = tex2D (_MainTex, i.uv);

                if(i.uv.x < _Percentages[0])
                    index = 0;
                else if(i.uv.x < _Percentages[0] + _Percentages[1])
                    index = 1;
                else if(i.uv.x < _Percentages[0] + _Percentages[1] + _Percentages[2])
                    index = 2;
                else
                    index = 3;

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);

                return c * _Colours[index].rgbr;
            }
            ENDCG
        }
    }
}
