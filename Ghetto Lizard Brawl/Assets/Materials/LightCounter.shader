// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "LightCounter"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Bool("Bool", Int) = 0
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Overlay+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform int _Bool;
		uniform float _Cutoff = 0.5;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 temp_cast_0 = (0.04).xxxx;
			float4 temp_cast_1 = (( 0.04 + 0.1 )).xxxx;
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 smoothstepResult5 = smoothstep( temp_cast_0 , temp_cast_1 , tex2D( _TextureSample0, uv_TextureSample0 ));
			float4 OpacityMask11 = smoothstepResult5;
			float4 color15 = IsGammaSpace() ? float4(0.3081415,0.3059456,0.3008741,1) : float4(0.07733504,0.07621743,0.07367248,1);
			float4 color3 = IsGammaSpace() ? float4(1.844303,1.844303,1.844303,0) : float4(3.84442,3.84442,3.84442,0);
			o.Emission = ( OpacityMask11 * ( ( color15 * ( 1.0 - _Bool ) ) + ( _Bool * color3 ) ) ).rgb;
			o.Alpha = 1;
			clip( OpacityMask11.r - _Cutoff );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18000
176;73;1457;653;1431.625;-228.5765;1;True;True
Node;AmplifyShaderEditor.CommentaryNode;24;-900.9863,-494.4375;Inherit;False;894.2744;476.0976;Comment;6;6;8;4;9;5;11;Opacity Map;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;25;-994.6006,25.03304;Inherit;False;1032.913;680.0308;Comment;9;1;20;22;12;3;23;21;15;2;Emission;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-781.9329,-243.9458;Inherit;False;Constant;_Float0;Float 0;1;0;Create;True;0;0;False;0;0.04;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-776.8113,-134.34;Inherit;False;Constant;_Float1;Float 1;1;0;Create;True;0;0;False;0;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;4;-850.9863,-444.4375;Inherit;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;-1;0000000000000000f000000000000000;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.IntNode;1;-944.6006,375.1869;Inherit;False;Property;_Bool;Bool;1;0;Create;True;0;0;False;0;0;0;0;1;INT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;9;-591.8112,-192.34;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;5;-460.757,-342.0851;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;3;-920.525,493.0638;Inherit;False;Constant;_Color0;Color 0;0;1;[HDR];Create;True;0;0;False;0;1.844303,1.844303,1.844303,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;21;-801.1451,288.8164;Inherit;False;1;0;INT;0;False;1;INT;0
Node;AmplifyShaderEditor.ColorNode;15;-908.2556,75.03303;Inherit;False;Constant;_Color1;Color 1;2;1;[HDR];Create;True;0;0;False;0;0.3081415,0.3059456,0.3008741,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-644.0095,166.633;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;INT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;11;-230.7119,-352.0503;Inherit;False;OpacityMask;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-639.0837,436.299;Inherit;True;2;2;0;INT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;12;-431.6053,138.8183;Inherit;False;11;OpacityMask;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;23;-412.9446,272.126;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-196.6872,164.3571;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RelayNode;18;137.7691,52.0009;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;16;80.65822,231.3673;Inherit;False;11;OpacityMask;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;277.1681,4.142907;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;LightCounter;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;False;Transparent;;Overlay;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;9;0;6;0
WireConnection;9;1;8;0
WireConnection;5;0;4;0
WireConnection;5;1;6;0
WireConnection;5;2;9;0
WireConnection;21;0;1;0
WireConnection;22;0;15;0
WireConnection;22;1;21;0
WireConnection;11;0;5;0
WireConnection;20;0;1;0
WireConnection;20;1;3;0
WireConnection;23;0;22;0
WireConnection;23;1;20;0
WireConnection;2;0;12;0
WireConnection;2;1;23;0
WireConnection;18;0;2;0
WireConnection;0;2;18;0
WireConnection;0;10;16;0
ASEEND*/
//CHKSM=8B4803A74A7A40AC71FB22E2936C15E877D3FA4C