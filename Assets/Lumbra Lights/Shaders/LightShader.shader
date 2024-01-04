Shader "Custom/LightFOV"
{

    //#define UNITY_SHADER_NO_UPGRADE 1
    Properties
    {
        _Color("Main Color", Color) = (1,1,1,1)
        _MainTex("Base (RGB) Trans (A)", 2D) = "" {}
        _Radius("Radius", Range(0.001, 500)) = 10
        //_PlayerPosition("Player Position", vector) = (0,0,0,0)
		_LightPosition("Light Position", vector) = (0,0,0,0)
		_Intense("Intensity", Range(0.001, 30)) = 5
    }
        SubShader
        {
            Tags {"Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True"}
            Blend SrcAlpha OneMinusSrcAlpha
            ColorMask RGBA
            ZWrite Off
			Lighting Off
			AlphaToMask Off //Queda to guapo
			Cull Off
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
                float4 _LightPosition;
 
                v2f vert(appdata_base v) {
                    v2f o;
					//o.pos = UnityObjectToClipPos(v.vertex);
                    o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                    o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                    o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                    return o;
                }
 
                float _Radius;
				float _Intense;
 
                fixed4 frag(v2f i) : SV_Target {
                    fixed4 col = tex2D(_MainTex, i.uv);
                    float dist = distance(i.worldPos, _LightPosition);
                    col.a = saturate((_Radius / (dist + _Intense))- 0.5f); //Si dist le restamos 2 crea efecto chulo para separar la luz con un aro
					col.rgb = _Color;
					col.a *= _Color.a;
                    return col;
                }
 
                ENDCG
            }
        }
}