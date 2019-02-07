Shader "Custom/GridShader" {
    Properties {
        _BackgroundColor ("Background Color", Color) = (0,0,0,0)
        _GridColor ("Grid Color", Color) = (1,1,1,1)
        _GridBase ("Units per Gridline", float) = 1
        _Sharpness ("Sharpness", float) = 0
        _DarkLine ("Every nth line darker", int) = 0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Lambert
        
        static const float PI =  float(3.141592653589793238462643383279503);

        struct Input {
            float3 worldPos;
        };
        int _DarkLine;
        float _GridBase, _Sharpness;
        fixed4 _BackgroundColor, _GridColor;

        void surf (Input IN, inout SurfaceOutput o) {
            o.Albedo = float4(lerp(_GridColor, _BackgroundColor,
                
                    pow(
                        min(
                            min(
                                abs(sin(IN.worldPos.x * PI / _GridBase)),
                                abs(sin(IN.worldPos.z * PI / _GridBase))
                            ),
                            abs(sin(IN.worldPos.y * PI / _GridBase))
                        ),
                        1.0f/abs(_Sharpness)
                    )
                    
                    +
                    
                    pow(
                        min(
                            min(
                                abs(sin(IN.worldPos.x * PI / (_GridBase * _DarkLine))),
                                abs(sin(IN.worldPos.z * PI / (_GridBase * _DarkLine)))
                            ),
                            abs(sin(IN.worldPos.y * PI / (_GridBase * _DarkLine)))
                        ),
                        1.0f/abs(_Sharpness)
                    )
                
                    / 2 
                
            ));
        }
        ENDCG
    }
    Fallback "Diffuse"
}
