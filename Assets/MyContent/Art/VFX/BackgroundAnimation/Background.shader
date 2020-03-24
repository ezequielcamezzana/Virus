// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Background"
{
	Properties
	{
		_MainTexture("MainTexture", 2D) = "white" {}
		_TextureFog("TextureFog", 2D) = "white" {}
		_MoveX("MoveX", Range( 0 , 0.3)) = 0.03
		_MoveY("MoveY", Range( 0 , 0.3)) = 0.03
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Opaque" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		
		
		Pass
		{
			Name "Unlit"
			Tags { "LightMode"="ForwardBase" }
			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord : TEXCOORD0;
			};

			uniform sampler2D _MainTexture;
			uniform float4 _MainTexture_ST;
			uniform sampler2D _TextureFog;
			uniform float _MoveX;
			uniform float _MoveY;

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				float2 uv_MainTexture = i.ase_texcoord.xy * _MainTexture_ST.xy + _MainTexture_ST.zw;
				float mulTime3 = _Time.y * _MoveX;
				float mulTime4 = _Time.y * _MoveY;
				float2 appendResult8 = (float2(mulTime3 , mulTime4));
				float2 uv06 = i.ase_texcoord.xy * float2( 1,1 ) + appendResult8;
				float4 blendOpSrc9 = tex2D( _MainTexture, uv_MainTexture );
				float4 blendOpDest9 = tex2D( _TextureFog, uv06 );
				float4 lerpBlendMode9 = lerp(blendOpDest9,( blendOpSrc9 * blendOpDest9 ),0.99);
				
				
				finalColor = ( saturate( lerpBlendMode9 ));
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=17700
0;23;1302;648;1201.582;285.3759;1.28985;True;True
Node;AmplifyShaderEditor.RangedFloatNode;11;-1401.509,78.36177;Inherit;False;Property;_MoveX;MoveX;2;0;Create;True;0;0;False;0;0.03;0;0;0.3;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-1396.35,154.4629;Inherit;False;Property;_MoveY;MoveY;3;0;Create;True;0;0;False;0;0.03;0;0;0.3;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;3;-1096.182,84.04283;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;4;-1092.186,159.0388;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;8;-899.8848,102.2387;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;6;-704.2125,54.86139;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-442.2093,25.83643;Inherit;True;Property;_TextureFog;TextureFog;1;0;Create;True;0;0;False;0;-1;23bad3c892edd409e9894fd3e5c94575;23bad3c892edd409e9894fd3e5c94575;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;10;-290.4282,239.3735;Inherit;False;Constant;_Alhpa;Alhpa;2;0;Create;True;0;0;False;0;0.99;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-442.2093,-207.1346;Inherit;True;Property;_MainTexture;MainTexture;0;0;Create;True;0;0;False;0;-1;c9f3eb3c0e5804503a9c826f2f9908dc;c9f3eb3c0e5804503a9c826f2f9908dc;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BlendOpsNode;9;-98.03711,0.07304525;Inherit;True;Multiply;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;179.6809,-17.2466;Float;False;True;-1;2;ASEMaterialInspector;100;1;Background;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;False;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;0
WireConnection;3;0;11;0
WireConnection;4;0;12;0
WireConnection;8;0;3;0
WireConnection;8;1;4;0
WireConnection;6;1;8;0
WireConnection;2;1;6;0
WireConnection;9;0;1;0
WireConnection;9;1;2;0
WireConnection;9;2;10;0
WireConnection;0;0;9;0
ASEEND*/
//CHKSM=4FF64C1EC9281312B6F96C429BF59E09A7B768FA