// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33361,y:32683,varname:node_3138,prsc:2|emission-2628-OUT;n:type:ShaderForge.SFN_Tex2d,id:9559,x:31011,y:32732,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_9559,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:22a17b56202f4e749a77a41e74d8500e,ntxv:0,isnm:False|UVIN-5225-UVOUT;n:type:ShaderForge.SFN_Multiply,id:5014,x:30623,y:32742,varname:node_5014,prsc:2|A-1949-OUT,B-2391-T;n:type:ShaderForge.SFN_Time,id:2391,x:30417,y:32816,varname:node_2391,prsc:2;n:type:ShaderForge.SFN_Panner,id:5225,x:30831,y:32732,varname:node_5225,prsc:2,spu:-0.1,spv:0|UVIN-4125-UVOUT,DIST-5014-OUT;n:type:ShaderForge.SFN_TexCoord,id:5061,x:31547,y:32559,varname:node_5061,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:6066,x:31806,y:32624,varname:node_6066,prsc:2|A-5061-UVOUT,B-9401-OUT;n:type:ShaderForge.SFN_Multiply,id:2301,x:31308,y:32761,varname:node_2301,prsc:2|A-9559-RGB,B-492-OUT;n:type:ShaderForge.SFN_Subtract,id:4781,x:32167,y:32624,varname:node_4781,prsc:2|A-5327-OUT,B-1742-OUT;n:type:ShaderForge.SFN_Tex2d,id:7862,x:32494,y:32616,ptovrint:False,ptlb:1112,ptin:_1112,varname:node_7862,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:392e165348dbfba44bad56610bde45e2,ntxv:0,isnm:False|UVIN-4781-OUT;n:type:ShaderForge.SFN_TexCoord,id:4125,x:30602,y:32531,varname:node_4125,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Slider,id:1949,x:30260,y:32726,ptovrint:False,ptlb:Time Value,ptin:_TimeValue,varname:node_1949,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2.5,max:5;n:type:ShaderForge.SFN_Power,id:5327,x:31990,y:32624,varname:node_5327,prsc:2|VAL-2166-OUT,EXP-6066-OUT;n:type:ShaderForge.SFN_Multiply,id:659,x:32874,y:32753,varname:node_659,prsc:2|A-9645-OUT,B-7862-RGB;n:type:ShaderForge.SFN_Slider,id:9645,x:32757,y:32663,ptovrint:False,ptlb:Light Multiply,ptin:_LightMultiply,varname:node_9645,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:5,max:10;n:type:ShaderForge.SFN_Power,id:2628,x:33070,y:32753,varname:node_2628,prsc:2|VAL-659-OUT,EXP-1635-OUT;n:type:ShaderForge.SFN_Slider,id:1635,x:32937,y:32962,ptovrint:False,ptlb:Light Power,ptin:_LightPower,varname:node_1635,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.53,max:3;n:type:ShaderForge.SFN_Power,id:9401,x:31547,y:32761,varname:node_9401,prsc:2|VAL-5588-OUT,EXP-2301-OUT;n:type:ShaderForge.SFN_ValueProperty,id:492,x:31069,y:32957,ptovrint:False,ptlb:Texture Value,ptin:_TextureValue,varname:node_492,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:5;n:type:ShaderForge.SFN_ValueProperty,id:5588,x:31140,y:32460,ptovrint:False,ptlb:Tex Bright,ptin:_TexBright,varname:node_5588,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.3;n:type:ShaderForge.SFN_ValueProperty,id:2166,x:31806,y:32541,ptovrint:False,ptlb:UVPower,ptin:_UVPower,varname:node_2166,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2.3;n:type:ShaderForge.SFN_ValueProperty,id:1742,x:31905,y:32817,ptovrint:False,ptlb:UVSubtract,ptin:_UVSubtract,varname:node_1742,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:5;proporder:9559-7862-1949-9645-1635-5588-492-2166-1742;pass:END;sub:END;*/

Shader "Shader Forge/NewShader" {
    Properties {
        _Texture ("Texture", 2D) = "white" {}
        _1112 ("1112", 2D) = "white" {}
        _TimeValue ("Time Value", Range(0, 5)) = 2.5
        _LightMultiply ("Light Multiply", Range(0, 10)) = 5
        _LightPower ("Light Power", Range(0, 3)) = 1.53
        _TexBright ("Tex Bright", Float ) = 0.3
        _TextureValue ("Texture Value", Float ) = 5
        _UVPower ("UVPower", Float ) = 2.3
        _UVSubtract ("UVSubtract", Float ) = 5
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform sampler2D _1112; uniform float4 _1112_ST;
            uniform float _TimeValue;
            uniform float _LightMultiply;
            uniform float _LightPower;
            uniform float _TextureValue;
            uniform float _TexBright;
            uniform float _UVPower;
            uniform float _UVSubtract;
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
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
////// Emissive:
                float4 node_2391 = _Time;
                float2 node_5225 = (i.uv0+(_TimeValue*node_2391.g)*float2(-0.1,0));
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_5225, _Texture));
                float3 node_4781 = (pow(_UVPower,(float3(i.uv0,0.0)+pow(_TexBright,(_Texture_var.rgb*_TextureValue))))-_UVSubtract);
                float4 _1112_var = tex2D(_1112,TRANSFORM_TEX(node_4781, _1112));
                float3 emissive = pow((_LightMultiply * _1112_var.rgb), _LightPower);
				float3 finalColor = emissive;// *_LightColor0.rgb;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
