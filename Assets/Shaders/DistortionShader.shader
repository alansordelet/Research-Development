Shader"Custom/DistortionShader"
{
    Properties
    {
        _MainTex("Base (RGB)", 2D) = "white" {}
        _DistortionAmount("Distortion Amount", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }


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

float _DistortionAmount;
sampler2D _MainTex;

v2f vert(appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = v.uv;
    return o;
}

fixed4 frag(v2f i) : SV_Target
{
                // Simple wave-based distortion
    float2 distortion = sin(i.uv * 10 + _Time.y) * _DistortionAmount;
    fixed4 col = tex2D(_MainTex, i.uv + distortion);
    return col;
}
            ENDCG
        }
    }
FallBack"Diffuse"
}
