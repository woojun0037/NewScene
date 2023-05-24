Shader "CustomRenderTexture/dark Custom Render Texture"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Main Tex", 2D) = "white" {}
        _BumpTex("Normal Tex", 2D) = "bump" {}
        _MeTTex("Metallic Tex", 2D) = "black" {}
        _AOTex("AO Tex", 2D) = "white" {}

        _AOP("AO power", Range(0,1)) = 1
        _EMTex("Emission Tex", 2D) = "black"{}
        _EMP("Emission Power", Range(0,1)) = 1
    }

        SubShader
        {
     Tags{ "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }
     LOD 200

            CGPROGRAM
            #pragma surface surf Standard fullforwardshadows
            #pragma target 3.0

            sampler2D _MainTex;
            sampler2D _BumpTex;
            sampler2D _MeTTex;
            sampler2D _AOTex;
            sampler2D _EMTex;

            struct Input {
                float2 uv_MainTex;
                float2 uv_BumpTex;
                float2 uv_MeTTex;
                float2 uv_AOTex;
                float2 uv_EMTex;
            };

            float _EMP;
            float _AOP;
            float4 _Color;

            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                float4 t = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                float4 m = tex2D(_MeTTex, IN.uv_MainTex);
                float4 b = tex2D(_BumpTex, IN.uv_MainTex);
                float4 e = tex2D(_EMTex, IN.uv_MainTex);
                float4 a = tex2D(_AOTex, IN.uv_MainTex);

                o.Albedo = t.rgb;
                o.Normal = UnpackNormal(b);
                o.Metallic = m.r;
                o.Smoothness = m.a;
                o.Occlusion = a.r + _AOP;
                o.Emission = e.rgb * _EMP;
                o.Alpha = t.a;
            }
            ENDCG
        }
            FallBack "Diffuse"
}