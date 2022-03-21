// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Mtree/Billboard"
{
	Properties
	{
		[Header(Albedo)]_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo", 2D) = "white" {}
		[Enum(Off,0,Front,1,Back,2)]_CullMode("Cull Mode", Int) = 2
		_Cutoff("Cutoff", Range( 0 , 1)) = 0.5
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Glossiness("Smoothness", Float) = 0
		[Header(Wind)]_GlobalWindInfluence("Global Wind Influence", Range( 0 , 1)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
		[Header(Forward Rendering Options)]
		[ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
		[ToggleOff] _GlossyReflections("Reflections", Float) = 1.0
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" }
		Cull [_CullMode]
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma shader_feature _SPECULARHIGHLIGHTS_OFF
		#pragma shader_feature _GLOSSYREFLECTIONS_OFF
		#pragma multi_compile __ LOD_FADE_CROSSFADE
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPosition;
		};

		uniform int _CullMode;
		uniform float _WindStrength;
		uniform float _GlobalWindInfluence;
		uniform float _RandomWindOffset;
		uniform float _WindPulse;
		uniform float _WindDirection;
		uniform int BillboardWindEnabled;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform float4 _Color;
		uniform float _Metallic;
		uniform float _Glossiness;
		uniform half _Cutoff;


		float2 DirectionalEquation( float _WindDirection )
		{
			float d = _WindDirection * 0.0174532924;
			float xL = cos(d) + 1 / 2;
			float zL = sin(d) + 1 / 2;
			return float2(zL,xL);
		}


		inline float Dither8x8Bayer( int x, int y )
		{
			const float dither[ 64 ] = {
				 1, 49, 13, 61,  4, 52, 16, 64,
				33, 17, 45, 29, 36, 20, 48, 32,
				 9, 57,  5, 53, 12, 60,  8, 56,
				41, 25, 37, 21, 44, 28, 40, 24,
				 3, 51, 15, 63,  2, 50, 14, 62,
				35, 19, 47, 31, 34, 18, 46, 30,
				11, 59,  7, 55, 10, 58,  6, 54,
				43, 27, 39, 23, 42, 26, 38, 22};
			int r = y * 8 + x;
			return dither[r] / 64; // same # of instructions as pre-dividing due to compiler magic
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float3 VAR_VertexPosition21_g1 = mul( unity_ObjectToWorld, float4( ase_vertex3Pos , 0.0 ) ).xyz;
			float3 break109_g1 = VAR_VertexPosition21_g1;
			float VAR_WindStrength43_g1 = ( _WindStrength * _GlobalWindInfluence );
			float4 transform37_g1 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float2 appendResult38_g1 = (float2(transform37_g1.x , transform37_g1.z));
			float dotResult2_g2 = dot( appendResult38_g1 , float2( 12.9898,78.233 ) );
			float lerpResult8_g2 = lerp( 0.8 , ( ( _RandomWindOffset / 2.0 ) + 0.9 ) , frac( ( sin( dotResult2_g2 ) * 43758.55 ) ));
			float VAR_RandomTime16_g1 = ( _Time.x * lerpResult8_g2 );
			float FUNC_Turbulence36_g1 = ( sin( ( ( VAR_RandomTime16_g1 * 40.0 ) - ( VAR_VertexPosition21_g1.z / 15.0 ) ) ) * 0.5 );
			float VAR_WindPulse274_g1 = _WindPulse;
			float FUNC_Angle73_g1 = ( VAR_WindStrength43_g1 * ( 1.0 + sin( ( ( ( ( VAR_RandomTime16_g1 * 2.0 ) + FUNC_Turbulence36_g1 ) - ( VAR_VertexPosition21_g1.z / 50.0 ) ) - ( v.color.r / 20.0 ) ) ) ) * sqrt( v.color.r ) * 0.2 * VAR_WindPulse274_g1 );
			float VAR_SinA80_g1 = sin( FUNC_Angle73_g1 );
			float VAR_CosA78_g1 = cos( FUNC_Angle73_g1 );
			float _WindDirection164_g1 = _WindDirection;
			float2 localDirectionalEquation164_g1 = DirectionalEquation( _WindDirection164_g1 );
			float2 break165_g1 = localDirectionalEquation164_g1;
			float VAR_xLerp83_g1 = break165_g1.x;
			float lerpResult118_g1 = lerp( break109_g1.x , ( ( break109_g1.y * VAR_SinA80_g1 ) + ( break109_g1.x * VAR_CosA78_g1 ) ) , VAR_xLerp83_g1);
			float3 break98_g1 = VAR_VertexPosition21_g1;
			float3 break105_g1 = VAR_VertexPosition21_g1;
			float VAR_zLerp95_g1 = break165_g1.y;
			float lerpResult120_g1 = lerp( break105_g1.z , ( ( break105_g1.y * VAR_SinA80_g1 ) + ( break105_g1.z * VAR_CosA78_g1 ) ) , VAR_zLerp95_g1);
			float3 appendResult122_g1 = (float3(lerpResult118_g1 , ( ( break98_g1.y * VAR_CosA78_g1 ) - ( break98_g1.z * VAR_SinA80_g1 ) ) , lerpResult120_g1));
			float3 FUNC_vertexPos123_g1 = appendResult122_g1;
			float3 temp_output_5_0_g1 = mul( unity_WorldToObject, float4( FUNC_vertexPos123_g1 , 0.0 ) ).xyz;
			float3 lerpResult61 = lerp( temp_output_5_0_g1 , ase_vertex3Pos , (float)BillboardWindEnabled);
			float3 OUT_VertexPos43 = lerpResult61;
			v.vertex.xyz = OUT_VertexPos43;
			float4 ase_screenPos = ComputeScreenPos( UnityObjectToClipPos( v.vertex ) );
			o.screenPosition = ase_screenPos;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float4 tex2DNode3 = tex2D( _MainTex, uv_MainTex );
			float4 VAR_AlbedoTexture53 = tex2DNode3;
			float4 OUT_Albedo45 = ( VAR_AlbedoTexture53 * _Color );
			o.Albedo = OUT_Albedo45.rgb;
			o.Metallic = _Metallic;
			float lerpResult55 = lerp( 0.0 , VAR_AlbedoTexture53.r , _Glossiness);
			float OUT_Smoothness58 = lerpResult55;
			o.Smoothness = OUT_Smoothness58;
			clip( tex2DNode3.a - _Cutoff);
			float temp_output_41_0_g3 = tex2DNode3.a;
			float4 ase_screenPos = i.screenPosition;
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float2 clipScreen45_g3 = ase_screenPosNorm.xy * _ScreenParams.xy;
			float dither45_g3 = Dither8x8Bayer( fmod(clipScreen45_g3.x, 8), fmod(clipScreen45_g3.y, 8) );
			dither45_g3 = step( dither45_g3, unity_LODFade.x );
			#ifdef LOD_FADE_CROSSFADE
				float staticSwitch40_g3 = ( temp_output_41_0_g3 * dither45_g3 );
			#else
				float staticSwitch40_g3 = temp_output_41_0_g3;
			#endif
			float OUT_Alpha46 = staticSwitch40_g3;
			o.Alpha = OUT_Alpha46;
		}

		ENDCG
	}
	Fallback "Diffuse"
}
/*ASEBEGIN
Version=17800
532;85;1008;681;1616.327;-421.2299;1.335005;True;False
Node;AmplifyShaderEditor.CommentaryNode;1;-1358.361,4.554405;Inherit;False;1661.862;599.5559;;10;45;46;47;12;9;4;3;8;2;53;Albedo / Alpha;1,0.1254902,0.1254902,1;0;0
Node;AmplifyShaderEditor.TexturePropertyNode;2;-1308.361,54.55442;Float;True;Property;_MainTex;Albedo;1;0;Create;False;0;0;False;0;None;None;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;3;-988.5101,68.42551;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;53;-625.1459,69.20842;Inherit;False;VAR_AlbedoTexture;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;59;-1309.772,1486.662;Inherit;False;1107.456;340.4749;;5;54;55;57;58;56;Smoothness;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-957.0894,434.1565;Half;False;Property;_Cutoff;Cutoff;3;0;Create;True;0;0;False;0;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;42;-1336.592,755.2562;Inherit;False;1173.464;527.1923;;5;41;25;23;43;61;Vertex Pos;0,1,0.08965516,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;56;-1259.772,1536.662;Inherit;False;53;VAR_AlbedoTexture;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.IntNode;25;-1289.592,1110.256;Inherit;False;Global;BillboardWindEnabled;BillboardWindEnabled;3;1;[Enum];Create;True;2;On;0;Off;1;0;False;0;0;0;0;1;INT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;57;-975.6139,1575.474;Inherit;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.ClipNode;12;-529.3309,372.2194;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;8;-897.5212,258.5437;Inherit;False;Property;_Color;Color;0;0;Create;True;0;0;False;1;Header(Albedo);1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;41;-1227.852,900.655;Inherit;False;Mtree Wind;6;;1;d710ffc7589a70c42a3e6c5220c6279d;7,269,1,281,0,272,0,255,0,282,0,280,0,278,0;0;1;FLOAT3;0
Node;AmplifyShaderEditor.PosVertexDataNode;23;-1242.157,969.1607;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;54;-1258.275,1712.136;Inherit;False;Property;_Glossiness;Smoothness;5;0;Create;False;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;55;-703.8078,1652.054;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;61;-926.8926,932.368;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-290.8997,132.6499;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;47;-283.9381,373.024;Inherit;False;LOD CrossFade;-1;;3;bbfabe35be0e79d438adaa880ee1b0aa;1,44,1;1;41;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;45;-74.94881,127.5761;Inherit;False;OUT_Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;50;890.5921,57.31693;Inherit;False;787.6993;508.11;;6;39;40;48;44;49;60;Output;0,0,0,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;43;-644.763,926.1811;Inherit;False;OUT_VertexPos;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;58;-442.3161,1648.562;Inherit;False;OUT_Smoothness;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;51;-1297.001,-467.6064;Inherit;False;275;251.3827;;2;6;28;Variables;1,0,0.7241378,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;46;-82.57029,367.8114;Inherit;False;OUT_Alpha;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;44;984.2899,450.4269;Inherit;False;43;OUT_VertexPos;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;48;1015.524,116.2522;Inherit;False;45;OUT_Albedo;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;60;977.1042,293.0038;Inherit;False;58;OUT_Smoothness;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-1247.001,-417.6064;Inherit;False;Constant;_MaskClipValue;Mask Clip Value;14;1;[HideInInspector];Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;40;940.592,205.6661;Inherit;False;Property;_Metallic;Metallic;4;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;49;1014.521,371.8521;Inherit;False;46;OUT_Alpha;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.IntNode;28;-1236.955,-331.2237;Inherit;False;Property;_CullMode;Cull Mode;2;1;[Enum];Create;True;3;Off;0;Front;1;Back;2;0;True;0;2;0;0;1;INT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;39;1411.291,107.3169;Float;False;True;-1;2;;0;0;Standard;Mtree/Billboard;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;TransparentCutout;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Absolute;0;;-1;-1;-1;-1;0;False;0;0;True;28;-1;0;True;6;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;2;0
WireConnection;53;0;3;0
WireConnection;57;0;56;0
WireConnection;12;0;3;4
WireConnection;12;1;3;4
WireConnection;12;2;4;0
WireConnection;55;1;57;0
WireConnection;55;2;54;0
WireConnection;61;0;41;0
WireConnection;61;1;23;0
WireConnection;61;2;25;0
WireConnection;9;0;53;0
WireConnection;9;1;8;0
WireConnection;47;41;12;0
WireConnection;45;0;9;0
WireConnection;43;0;61;0
WireConnection;58;0;55;0
WireConnection;46;0;47;0
WireConnection;39;0;48;0
WireConnection;39;3;40;0
WireConnection;39;4;60;0
WireConnection;39;9;49;0
WireConnection;39;11;44;0
ASEEND*/
//CHKSM=21534B5DE0EC7C473BC29889ADFA56D0EEB2E616