Shader"Custom/SimpleDistortion"
{
    Properties
    {
        _MainTex("Texture", 2D) = "Red" {}
        _Distortion("Distortion", Range(0, 10)) = 5
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

sampler2D _MainTex;
float _Distortion;

v2f vert(appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = v.uv;
    return o;
}

fixed4 frag(v2f i) : SV_Target
{
    float2 distortedUV = i.uv;
    distortedUV.x += sin(distortedUV.y * 20.0 + _Time.y) * _Distortion;
    distortedUV.y += cos(distortedUV.x * 20.0 + _Time.y) * _Distortion;
    fixed4 col = tex2D(_MainTex, distortedUV);
    return col;
}
            ENDCG
        }
    }
}
