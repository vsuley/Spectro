// GLOBAL PARAMETERS TO BE SET FROM C#

// Basic matrices required for the shader to function.
float4x4 World;
float4x4 View;
float4x4 Projection;

// Phong shading
float4 LightPosition;
float LightStrength;
float AmbientReflectance;
float DiffuseReflectance;
float SpecularReflectance;
float Shininess;
float4 SpecularColor;
float3 CameraPosition;
float4 FinalRGB;
float4 MaterialRGB;
float4 LightColor;

// Full Spectral rendering
float3 TristimulusValues;

// Reinhard tone mapping variables.
float AverageLogLuminance;
float Key;
//float Lw_a;
//float LogTerm;

// GLOBAL CONSTANTS

// General

float AttenuationConstant = 0.0;
bool HDR = false;

// Tone mapping constants
float LD_MAX = 100.0;
float LW_MAX = 1000.0;

// DATA STRUCTURES
struct VertexShaderInput
{
    float4 Position : POSITION0;
    float4 Normal : NORMAL0;
    float2 TexCoords : TEXCOORD0;
};

// Structure for the Vertex shader to spit output.
// These values will automatically be interpolated and passed on to the pixel shader.
struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TexCoords : TEXCOORD0;
    float3 Normal : TEXCOORD1;
    float3 WorldPos : TEXCOORD2;
    float4 VertexColor : COLOR0;
};

// FUNCTION DECLARATIONS
float3x3 ColorTransformationMatrix ();
float3 ConvertXYZtoRGB(float3x3 transformationMatrix, float3 tristimulusValues);

// THE SHADING METHODS
// Vertex Shader
VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    // "Multiplication will be done in the pre-shader - so no cost per vertex"
    float4x4 viewprojection = mul(View, Projection);
    float4 posWorld = mul(input.Position, World);
    output.Position = mul(posWorld, viewprojection);
    output.TexCoords = input.TexCoords;
    
    // Passing information on to do both specular AND diffuse calculation in pixel shader
    output.Normal = mul(input.Normal, (float3x3)World);
    output.WorldPos = posWorld;

    // Use the vertex color calculated by program.    
    output.VertexColor = FinalRGB;

    return output;
}

// Pixel Shader
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    float4 outputColor;

    // Some basic ingredients.
    float3 lightVector = normalize(LightPosition - input.WorldPos);
    float3 reflectedRay = reflect( -lightVector, input.Normal);
    float3 viewer = normalize(-input.WorldPos);
    
    // Calculate diffuse component.
    // 
    float diffuseLightIntensity = LightStrength * (AmbientReflectance + DiffuseReflectance * saturate( dot(lightVector, input.Normal)));
    
    // Calculate specular component
    //
    float specularIntensity = LightStrength * SpecularReflectance * pow( saturate( dot(reflectedRay, viewer)), Shininess);

    if (HDR)
    {
        // Calculate the Ld(x,y)
        float Lxy, Ldxy;
        Lxy = Key / AverageLogLuminance * diffuseLightIntensity;
        Ldxy = Lxy / (1.0 + Lxy);
        outputColor = Ldxy * input.VertexColor;
    }

    else 
    {
        outputColor = saturate(diffuseLightIntensity * input.VertexColor + saturate(specularIntensity  * SpecularColor));
    }

    // Not sure why I'm having to do this, but my alpha seems to be messed up in some calculation.
    outputColor.a = 1;

    return outputColor;
}


technique TechniqueWithoutTexture
{
    pass Pass1
    {
        // TODO: set renderstates here.

        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }

}

// Utility Methods.
float3x3 ColorTransformationMatrix ()
{
    float3x3 colorTransformationMatrix;
    // Note that this is a pre-inverted matrix. As in, if you refer to a colorimetrix equation, the 
    // transformation matrix will have values Xrmax....Zbmax but will have to be inverted, this matrix
    // here contains results of the inversion. It has been made for a typical LCD display.
    // 
    colorTransformationMatrix[0] = float3(0.030747121,	-0.0152383,	-0.003660673);
    colorTransformationMatrix[1] = float3(-0.011160431,	0.02059972,	 7.59E-06);
    colorTransformationMatrix[2] = float3(0.000768247,	-0.00227443, 0.010599771);
    
    return colorTransformationMatrix;
}


float3 ConvertXYZtoRGB(float3x3 transformationMatrix, float3 tristimulusValues)
{
    float3 rgbValues = mul(transformationMatrix, tristimulusValues);
    return rgbValues;
}
