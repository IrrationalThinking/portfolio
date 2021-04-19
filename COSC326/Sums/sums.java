import java.lang.Math;
public class sums{
    public static float harmonicSingle(int x){
	float total = 0;
	for(int i = 1; i <= x; i++){
	    total += 1.0/i;
	}
	//System.out.println("harmonicSingle");
	System.out.println(total);
	return 0;
    }
    public static double harmonicDouble(int x){
	double total = 0.0;
	for(int i = 1; i <= x; i++){
	    total += 1.0/i;
	}
	//System.out.println("harmonicDouble");
	System.out.println(total);
	return 0;
    }
        public static float harmonicSingleB(int x){
	float total = 0;
	for(int i = x; i > 0; i--){
	    total += 1.0/i;
	}
	//System.out.println("harmonicSingle backwards");
	System.out.println(total);
	return 0;
    }
    public static double harmonicDoubleB(int x){
	double total = 0.0;
	for(int i = x; i > 0; i--){
	    total += 1.0/i;
	}
	//System.out.println("harmonicDouble backwards");
	System.out.println(total);
	return 0;
    }

        public static float cosineSingle(float x, int decimal){
	float t = 1;
	float total = 1;
	for(int i = 1; i < decimal+1; i++){
	    t = t *(float)(Math.pow((-1),(2*i-1))*x*x/(2*i*(2*i-1)));
	    //total += (((Math.pow(x, 2)(-1)(2*i-1))x*x)/(2*i(2*i-1)));
	    //total += ((Math.pow(x, 2))/((2*i)*(2*i-1)));
	    total += t;
	}
	System.out.println(total);
	return 0;
    }
    public static double cosineDouble(double x, int decimal){
	double t = 1;
	double total = 1;
	for(int i = 1; i < decimal+1; i++){
	    t = t * (Math.pow((-1),(2*i-1))*x*x/(2*i*(2*i-1)));
	    //total += (((Math.pow(x, 2)(-1)(2*i-1))x*x)/(2*i(2*i-1)));
	    //total += ((Math.pow(x, 2))/((2*i)*(2*i-1)));
	    total += t;
	}
	System.out.println(total);
	return 0;
    }
    public static float cosineSingleBuiltin(float x){
	//System.out.println((float)Math.cos(Math.toRadians(x)));
	System.out.println((float)Math.cos(x));
	return 0;
    }
    public static double cosineDoubleBuiltin(double x){
	System.out.println(Math.cos(x));
	//System.out.println(Math.cos(Math.toRadians(x)));
	return 0;
    }
    public static float identityProof(float f, float g){
	float total = 0;
	//	System.out.print("Value for f is ");
	//	System.out.println(f);
	total = (((f/g) - f * g)*g+f*g*g);
	//System.out.print("Value for total is ");
	//System.out.println(total);
	//if(total != f){
	    //System.out.print("g is equal to ");
	System.out.printf("%.2f", g);
	System.out.print("      ");
	//System.out.print("total is not equal to f total is ");
	//System.out.print(" and f is ");
	System.out.printf("%.3f", f);
	System.out.print("      ");
	System.out.println(total);
	//	}
	return 0;
    }
    public static void main(String[] args){
	/*System.out.println("harmonic single precisions values");
	System.out.print("Forwards calculation is ");
	harmonicSingle(5000);
	System.out.print("Backward calculation is ");
	harmonicSingleB(5000);
	System.out.println("harmonic double precision values");
	System.out.print("Forwards calculation is ");
	harmonicDouble(5000);
	System.out.print("Backward calculation is ");
	harmonicDoubleB(5000);*/
	/*for(int i = 1; i < 8; i = i + 1){
	    System.out.println("harmonic single precisions values " + i);
	    System.out.print("Forwards calculation is ");
	    harmonicSingle(i);
	    System.out.print("Backward calculation is ");
	    harmonicSingleB(i);
	    System.out.println("harmonic double precision values");
	    System.out.print("Forwards calculation is ");
	    harmonicDouble(i);
	    System.out.print("Backward calculation is ");
	    harmonicDoubleB(i);
	    }*/
	/*System.out.println("cosine single precision 0.998");
	  cosineSingle(0.998f, 5000);*/
	/*cosineSingle(0.995f, 5000);
	cosineSingle(0.994f, 5000);
	cosineSingle(12.345f, 5000);*/
	/*	System.out.println("cosine double precision 0.998");
	cosineDouble(0.998, 5000);
	/*	cosineDouble(0.995, 5000);
	cosineDouble(0.994, 5000);
	cosineDouble(12.345, 5000);*/
	/*	System.out.println("cosine single precision 0.995");
       	cosineSingle(0.995f, 5000);
	System.out.println("cosine double precision 0.995");
	cosineDouble(0.995, 5000);
	System.out.println("cosine single precision 0.994");
       	cosineSingle(0.994f, 5000);
	System.out.println("cosine double precision 0.994");
	cosineDouble(0.994, 5000);
	System.out.println("cosine single precision 12.345");
       	cosineSingle(12.345f, 5000);
	System.out.println("cosine double precision 12.345");
	cosineDouble(12.345, 5000);*/
       	/*System.out.println("cosine series builtin function");
	System.out.println("cosine single precision 0.998");
	cosineSingleBuiltin(0.998f);
	System.out.println("cosine double precision 0.998");
	cosineDoubleBuiltin(0.998);
	System.out.println("cosine single precision 0.995");
	cosineSingleBuiltin(0.995f);
	System.out.println("cosine double precision 0.995");
	cosineDoubleBuiltin(0.995);
	System.out.println("cosine single precision 0.994");
	cosineSingleBuiltin(0.994f);
	System.out.println("cosine double precision 0.994");
	cosineDoubleBuiltin(0.994);
	System.out.println("cosine single precision 12.345");
	cosineSingleBuiltin(12.345f);
	System.out.println("cosine double precision 12.345");
	cosineDoubleBuiltin(12.345);*/
	/*float g = 0.5f;
	  float f = 50f;*/
	System.out.println("expected single precision values");
	System.out.println("g          f          total");
	/*while(g < 50.5){
	    identityProof(g, f);
	    g += 0.375;
	    f -= 0.1;
	    
	    }*/
	identityProof(10, 0.1f);
	identityProof(10, 0.125f);
	identityProof(10, 0.25f);
	identityProof(10, 0.5f);
	identityProof(0.1f, 10);
	identityProof(0.125f, 10);
	identityProof(0.25f, 10);
	identityProof(0.5f, 10);
	/*System.out.println("looping test");
	System.out.println("g    f   total");
	float g = 10000;
	//	for(float g = 1; g < 200f; g++){
	for(float f = 100000; f < 110000f; f= f + 1){
	    if(g != 0){
		g -= 1;
	    }
	    identityProof(f, g);
	    
		//  }
		}*/
    }
}
