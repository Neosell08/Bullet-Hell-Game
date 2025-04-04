Shader "Custom/CRTShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #include "CRTInclude.hlsl"
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        fixed4 Blur(float2 uv)
        {
            float2 offset = float2(0.0015, 0); // Offset for blur
            return (
                tex2D(_MainTex, uv) +
                tex2D(_MainTex, uv + offset) +
                tex2D(_MainTex, uv - offset) +
                tex2D(_MainTex, uv + float2(0, offset.y)) +
                tex2D(_MainTex, uv - float2(0, offset.y))
            ) / 5;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Apply blur to the texture
            fixed4 blurredColor = Blur(IN.uv_MainTex);

            // Set Albedo color
            // o.Albedo = blurredColor.rgb;
            o.Albedo = float3(1.0, 0.0, 0.0);
            o.Alpha = blurredColor.a;
        }
       
        ENDCG
    }
    FallBack "Diffuse"
}
