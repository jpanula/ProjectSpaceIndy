Shader "Custom/CeilingShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        X_Scaling ("X Scale", float) = 1
        Y_Scaling ("Y Scale", float) = 1
        X_Offset ("X Offset", float) = 0
        Y_Offset ("Y Offset", float) = 0
        
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Lambert

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };
        
        sampler2D _MainTex;
        float X_Scaling;
        float Y_Scaling;
        float X_Offset;
        float Y_Offset;
        

        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Albedo = tex2D (_MainTex, IN.worldPos.xz / float2(X_Scaling, Y_Scaling) + float2(X_Offset, Y_Offset) / 100);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
