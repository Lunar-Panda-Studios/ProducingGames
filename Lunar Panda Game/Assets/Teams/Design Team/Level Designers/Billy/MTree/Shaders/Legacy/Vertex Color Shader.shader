// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Hidden/Mtree/VertexColorShader"
{
	Properties
	{
		[Header(Wind)]_GlobalWindInfluence("Global Wind Influence", Range( 0 , 1)) = 1
		[Enum(Off,0,Front,1,Back,2)]_CullMode("Cull Mode", Int) = 2
		[HideInInspector] __dirty( "", Int ) = 1
		[Header(Forward Rendering Options)]
		[ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
		[ToggleOff] _GlossyReflections("Reflections", Float) = 1.0
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull [_CullMode]
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma shader_feature _SPECULARHIGHLIGHTS_OFF
		#pragma shader_feature _GLOSSYREFLECTIONS_OFF
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float4 vertexColor : COLOR;
		};

		uniform int _CullMode;
		uniform float _WindStrength;
		uniform float _GlobalWindInfluence;
		uniform float _RandomWindOffset;
		uniform float _WindPulse;
		uniform float _WindDirection;


		float2 DirectionalEquation( float _WindDirection )
		{
			float d = _WindDirection * 0.0174532924;
			float xL = cos(d) + 1 / 2;
			float zL = sin(d) + 1 / 2;
			return float2(zL,xL);
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float3 VAR_VertexPosition21_g2 = mul( unity_ObjectToWorld, float4( ase_vertex3Pos , 0.0 ) ).xyz;
			float3 break109_g2 = VAR_VertexPosition21_g2;
			float VAR_WindStrength43_g2 = ( _WindStrength * _GlobalWindInfluence );
			float4 transform37_g2 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float2 appendResult38_g2 = (float2(transform37_g2.x , transform37_g2.z));
			float dotResult2_g3 = dot( appendResult38_g2 , float2( 12.9898,78.233 ) );
			float lerpResult8_g3 = lerp( 0.8 , ( ( _RandomWindOffset / 2.0 ) + 0.9 ) , frac( ( sin( dotResult2_g3 ) * 43758.55 ) ));
			float VAR_RandomTime16_g2 = ( _Time.x * lerpResult8_g3 );
			float FUNC_Turbulence36_g2 = ( sin( ( ( VAR_RandomTime16_g2 * 40.0 ) - ( VAR_VertexPosition21_g2.z / 15.0 ) ) ) * 0.5 );
			float VAR_WindPulse274_g2 = _WindPulse;
			float FUNC_Angle73_g2 = ( VAR_WindStrength43_g2 * ( 1.0 + sin( ( ( ( ( VAR_RandomTime16_g2 * 2.0 ) + FUNC_Turbulence36_g2 ) - ( VAR_VertexPosition21_g2.z / 50.0 ) ) - ( v.color.r / 20.0 ) ) ) ) * sqrt( v.color.r ) * 0.2 * VAR_WindPulse274_g2 );
			float VAR_SinA80_g2 = sin( FUNC_Angle73_g2 );
			float VAR_CosA78_g2 = cos( FUNC_Angle73_g2 );
			float _WindDirection164_g2 = _WindDirection;
			float2 localDirectionalEquation164_g2 = DirectionalEquation( _WindDirection164_g2 );
			float2 break165_g2 = localDirectionalEquation164_g2;
			float VAR_xLerp83_g2 = break165_g2.x;
			float lerpResult118_g2 = lerp( break109_g2.x , ( ( break109_g2.y * VAR_SinA80_g2 ) + ( break109_g2.x * VAR_CosA78_g2 ) ) , VAR_xLerp83_g2);
			float3 break98_g2 = VAR_VertexPosition21_g2;
			float3 break105_g2 = VAR_VertexPosition21_g2;
			float VAR_zLerp95_g2 = break165_g2.y;
			float lerpResult120_g2 = lerp( break105_g2.z , ( ( break105_g2.y * VAR_SinA80_g2 ) + ( break105_g2.z * VAR_CosA78_g2 ) ) , VAR_zLerp95_g2);
			float3 appendResult122_g2 = (float3(lerpResult118_g2 , ( ( break98_g2.y * VAR_CosA78_g2 ) - ( break98_g2.z * VAR_SinA80_g2 ) ) , lerpResult120_g2));
			float3 FUNC_vertexPos123_g2 = appendResult122_g2;
			float3 break221_g2 = FUNC_vertexPos123_g2;
			half FUNC_SinFunction195_g2 = sin( ( ( VAR_RandomTime16_g2 * 200.0 * ( 0.2 + v.color.g ) ) + ( v.color.g * 10.0 ) + FUNC_Turbulence36_g2 + ( VAR_VertexPosition21_g2.z / 2.0 ) ) );
			float temp_output_202_0_g2 = ( FUNC_SinFunction195_g2 * v.color.b * ( FUNC_Angle73_g2 + ( VAR_WindStrength43_g2 / 200.0 ) ) );
			float lerpResult203_g2 = lerp( 0.0 , temp_output_202_0_g2 , VAR_xLerp83_g2);
			float lerpResult196_g2 = lerp( 0.0 , temp_output_202_0_g2 , VAR_zLerp95_g2);
			float3 appendResult197_g2 = (float3(( break221_g2.x + lerpResult203_g2 ) , break221_g2.y , ( break221_g2.z + lerpResult196_g2 )));
			float3 OUT_Grass_Standalone245_g2 = appendResult197_g2;
			float3 temp_output_5_0_g2 = mul( unity_WorldToObject, float4( OUT_Grass_Standalone245_g2 , 0.0 ) ).xyz;
			v.vertex.xyz = temp_output_5_0_g2;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Albedo = i.vertexColor.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
}
/*ASEBEGIN
Version=17800
184;64;1008;645;1888.767;600.0108;2.064041;True;False
Node;AmplifyShaderEditor.CommentaryNode;22;-976.9675,61.41877;Inherit;False;275.1868;247.6812;;1;19;Vertex Position;0,1,0.09019608,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;24;-1409.197,-326.6083;Inherit;False;674.4768;271.9942;;1;1;Albedo;1,0.1254902,0.1254902,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;23;-1397.893,68.09306;Inherit;False;275;165;;1;17;Variables;1,0,0.7254902,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;25;-334.9035,-308.0691;Inherit;False;313;505;;1;16;Output;0,0,0,1;0;0
Node;AmplifyShaderEditor.IntNode;17;-1372.171,118.5078;Inherit;False;Property;_CullMode;Cull Mode;9;1;[Enum];Create;True;3;Off;0;Front;1;Back;2;0;True;0;2;0;0;1;INT;0
Node;AmplifyShaderEditor.FunctionNode;19;-925.9949,163.2096;Inherit;False;Mtree Wind;0;;2;d710ffc7589a70c42a3e6c5220c6279d;7,282,0,280,0,278,0,255,4,269,1,281,0,272,0;0;1;FLOAT3;0
Node;AmplifyShaderEditor.VertexColorNode;1;-1338.694,-258.3241;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;16;-284.9035,-258.0691;Float;False;True;-1;2;;0;0;Standard;Hidden/Mtree/VertexColorShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Absolute;0;;-1;-1;-1;-1;0;False;0;0;True;17;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;16;0;1;0
WireConnection;16;11;19;0
ASEEND*/
//CHKSM=65AF6971E9E94B4582BA6172FC56574B9405FF2C