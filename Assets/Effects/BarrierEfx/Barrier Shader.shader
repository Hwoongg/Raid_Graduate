// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.1280277,fgcg:0.1953466,fgcb:0.2352941,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:6486,x:33215,y:32757,varname:node_6486,prsc:2|emission-6716-OUT;n:type:ShaderForge.SFN_TexCoord,id:6409,x:31721,y:32533,varname:node_6409,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:6365,x:31922,y:33029,varname:node_6365,prsc:2|A-6409-UVOUT,B-4472-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4472,x:31726,y:33049,ptovrint:False,ptlb:Coored Multi_2,ptin:_CooredMulti_2,varname:node_4472,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Time,id:2215,x:32139,y:32697,varname:node_2215,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:1578,x:32139,y:32604,ptovrint:False,ptlb:Coored Multi_1,ptin:_CooredMulti_1,varname:node_1578,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Multiply,id:1389,x:32319,y:32537,varname:node_1389,prsc:2|A-6409-UVOUT,B-1578-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8402,x:32139,y:32852,ptovrint:False,ptlb:Time_1,ptin:_Time_1,varname:node_8402,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:4017,x:32319,y:32697,varname:node_4017,prsc:2|A-2215-T,B-8402-OUT;n:type:ShaderForge.SFN_Tex2d,id:3128,x:32665,y:32537,ptovrint:False,ptlb:Texture_1,ptin:_Texture_1,varname:node_3128,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:eb2dbc3ed1051da45b8ea9aa576a0607,ntxv:2,isnm:False|UVIN-3880-UVOUT;n:type:ShaderForge.SFN_Panner,id:3880,x:32501,y:32537,varname:node_3880,prsc:2,spu:0.1,spv:0.1|UVIN-1389-OUT,DIST-4017-OUT;n:type:ShaderForge.SFN_Tex2d,id:9268,x:32139,y:33029,ptovrint:False,ptlb:Texture_2,ptin:_Texture_2,varname:node_9268,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:4bfbed036b5be88479bbe708e3fcdde7,ntxv:0,isnm:False|UVIN-6365-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4962,x:32139,y:33224,ptovrint:False,ptlb:Color Value,ptin:_ColorValue,varname:node_4962,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:861,x:32379,y:33175,varname:node_861,prsc:2|A-9268-RGB,B-4962-OUT,C-7664-RGB,D-7664-A;n:type:ShaderForge.SFN_Color,id:7664,x:32139,y:33309,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7664,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.5939897,c3:0.2028302,c4:1;n:type:ShaderForge.SFN_Slider,id:5272,x:32004,y:33483,ptovrint:False,ptlb:Fresnel,ptin:_Fresnel,varname:node_5272,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2.747495,max:5;n:type:ShaderForge.SFN_Fresnel,id:5361,x:32344,y:33455,varname:node_5361,prsc:2|EXP-5272-OUT;n:type:ShaderForge.SFN_Multiply,id:4621,x:32571,y:33175,varname:node_4621,prsc:2|A-861-OUT,B-5361-OUT;n:type:ShaderForge.SFN_Multiply,id:6716,x:32945,y:32855,varname:node_6716,prsc:2|A-3128-RGB,B-4621-OUT;proporder:4472-1578-8402-3128-9268-4962-7664-5272;pass:END;sub:END;*/

Shader "Unlit/Barrier Shader" {
    Properties {
        _CooredMulti_2 ("Coored Multi_2", Float ) = 3
        _CooredMulti_1 ("Coored Multi_1", Float ) = 3
        _Time_1 ("Time_1", Float ) = 1
        _Texture_1 ("Texture_1", 2D) = "black" {}
        _Texture_2 ("Texture_2", 2D) = "white" {}
        _ColorValue ("Color Value", Float ) = 1
        _Color ("Color", Color) = (1,0.5939897,0.2028302,1)
        _Fresnel ("Fresnel", Range(0, 5)) = 2.747495
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 100
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _CooredMulti_2;
            uniform float _CooredMulti_1;
            uniform float _Time_1;
            uniform sampler2D _Texture_1; uniform float4 _Texture_1_ST;
            uniform sampler2D _Texture_2; uniform float4 _Texture_2_ST;
            uniform float _ColorValue;
            uniform float4 _Color;
            uniform float _Fresnel;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 node_2215 = _Time;
                float2 node_3880 = ((i.uv0*_CooredMulti_1)+(node_2215.g*_Time_1)*float2(0.1,0.1));
                float4 _Texture_1_var = tex2D(_Texture_1,TRANSFORM_TEX(node_3880, _Texture_1));
                float2 node_6365 = (i.uv0*_CooredMulti_2);
                float4 _Texture_2_var = tex2D(_Texture_2,TRANSFORM_TEX(node_6365, _Texture_2));
                float3 emissive = (_Texture_1_var.rgb*((_Texture_2_var.rgb*_ColorValue*_Color.rgb*_Color.a)*pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnel)));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
