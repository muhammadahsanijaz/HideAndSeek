Shader "Custom/Stencil Mask"
{

    //#define UNITY_SHADER_NO_UPGRADE 1
    Properties
    {
        _Color("Main Color", Color) = (1,1,1,1)
        _MainTex("Base (RGB) Trans (A)", 2D) = "Black" {}
        _Radius("Radius", Range(0.001, 500)) = 10
        _PlayerPosition("Player Position", vector) = (0,0,0,0)
    }
        SubShader
        {
            Tags {"Queue" = "Transparent-1" "RenderType" = "Transparent"}
 
            Blend SrcAlpha OneMinusSrcAlpha
            ColorMask RGBA
            ZWrite off
			Lighting Off
            LOD 200
            Stencil
            {
                Ref 1
                Pass replace
            }
            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"
 
                struct v2f {
                    float4 pos : SV_POSITION;
                    float2 uv : TEXCOORD0;
                    float4 worldPos : TEXCOORD1;
                };
                fixed4 _Color;
                sampler2D _MainTex;
                float4 _MainTex_ST;
                float4 _PlayerPosition;
 
                v2f vert(appdata_base v) {
                    v2f o;
                    o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                    o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                    o.worldPos = mul(unity_ObjectToWorld, v.vertex);
					
                    return o;
                };
 
                float _Radius;
 
                fixed4 frag(v2f i) : SV_Target {
                    fixed4 col = tex2D(_MainTex, i.uv);
                    float dist = distance(i.worldPos, _PlayerPosition);
                    col.a = saturate((dist / _Radius) +0.0f);
					col.rgb = _Color;
					col.a *= _Color.a;
                    return col;
                };
 
                ENDCG
            }
        }
}