// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:False,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:33331,y:32657,varname:node_4795,prsc:2|diff-3792-RGB,emission-4943-OUT,clip-628-OUT;n:type:ShaderForge.SFN_Tex2d,id:1041,x:31492,y:33090,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_1041,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:eb2dbc3ed1051da45b8ea9aa576a0607,ntxv:0,isnm:False;n:type:ShaderForge.SFN_RemapRange,id:4938,x:31732,y:32929,varname:node_4938,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-2087-OUT;n:type:ShaderForge.SFN_OneMinus,id:2087,x:31589,y:32929,varname:node_2087,prsc:2|IN-3159-A;n:type:ShaderForge.SFN_Add,id:628,x:31824,y:33112,varname:node_628,prsc:2|A-4938-OUT,B-1041-R;n:type:ShaderForge.SFN_RemapRange,id:1142,x:31909,y:32617,varname:node_1142,prsc:2,frmn:0,frmx:1,tomn:-8,tomx:8|IN-628-OUT;n:type:ShaderForge.SFN_Clamp01,id:8286,x:32074,y:32617,varname:node_8286,prsc:2|IN-1142-OUT;n:type:ShaderForge.SFN_OneMinus,id:3003,x:32275,y:32681,varname:node_3003,prsc:2|IN-8286-OUT;n:type:ShaderForge.SFN_Append,id:5558,x:32453,y:32755,varname:node_5558,prsc:2|A-3003-OUT,B-9786-OUT;n:type:ShaderForge.SFN_Vector1,id:9786,x:32143,y:32961,varname:node_9786,prsc:2,v1:0;n:type:ShaderForge.SFN_Tex2d,id:8416,x:32626,y:32755,ptovrint:False,ptlb:Gardation,ptin:_Gardation,varname:node_8416,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d4aa5ca8733e3c949af44fa2202e4389,ntxv:0,isnm:False|UVIN-5558-OUT;n:type:ShaderForge.SFN_Tex2d,id:3792,x:32626,y:32546,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:node_3792,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7d210b28eb7b6684b8f3c0fac41c2de1,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:8123,x:32875,y:32843,varname:node_8123,prsc:2|A-8416-RGB,B-3662-OUT;n:type:ShaderForge.SFN_Slider,id:3662,x:32480,y:32985,ptovrint:False,ptlb:Intensify,ptin:_Intensify,varname:node_3662,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:10;n:type:ShaderForge.SFN_Multiply,id:4943,x:33074,y:32843,varname:node_4943,prsc:2|A-8123-OUT,B-4170-RGB;n:type:ShaderForge.SFN_VertexColor,id:3159,x:31202,y:32847,varname:node_3159,prsc:2;n:type:ShaderForge.SFN_VertexColor,id:4170,x:32820,y:33093,varname:node_4170,prsc:2;proporder:1041-3792-3662-8416;pass:END;sub:END;*/

Shader "Shader Forge/Mag Fade Shader" {
    Properties {
        _Noise ("Noise", 2D) = "white" {}
        _Diffuse ("Diffuse", 2D) = "white" {}
        _Intensify ("Intensify", Range(0, 10)) = 1
        _Gardation ("Gardation", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform sampler2D _Gardation; uniform float4 _Gardation_ST;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float _Intensify;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(i.uv0, _Noise));
                float node_628 = (((1.0 - i.vertexColor.a)*2.0+-1.0)+_Noise_var.r);
                clip(node_628 - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float3 diffuseColor = _Diffuse_var.rgb;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float2 node_5558 = float2((1.0 - saturate((node_628*16.0+-8.0))),0.0);
                float4 _Gardation_var = tex2D(_Gardation,TRANSFORM_TEX(node_5558, _Gardation));
                float3 emissive = ((_Gardation_var.rgb*_Intensify)*i.vertexColor.rgb);
/// Final Color:
                float3 finalColor = diffuse + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform sampler2D _Gardation; uniform float4 _Gardation_ST;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float _Intensify;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(i.uv0, _Noise));
                float node_628 = (((1.0 - i.vertexColor.a)*2.0+-1.0)+_Noise_var.r);
                clip(node_628 - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float3 diffuseColor = _Diffuse_var.rgb;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
