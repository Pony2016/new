#include <Wire.h>                  
#include<Keypad.h>                 
#include <Servo.h>                 
#include <SPI.h>                   
#include <MFRC522.h>               
#include <LiquidCrystal_I2C.h>     
#define SS_PIN 10                  
#define RST_PIN 9                  
MFRC522 mfrc522(SS_PIN,RST_PIN);   
unsigned long time_now = 0;        
LiquidCrystal_I2C lcd(0x27,16,2);  
const byte hang = 4;               
const byte lie = 4;                
char hexaKeys[hang][lie]={         
  {'A','3','2','1'},
  {'B','6','5','4'},
  {'C','9','8','7'},
  {'D','#','0','*'}
};
byte hangjiao[hang] = {4,5,6,7};         
byte liejiao[lie] = {1,0,2,3};           
char gaimi[6]{'0','0','0','0','0','0'};  
char mima[6] = {'1','2','3','A','B','C'};
char changcard[6]  {'1','1','1','1','1','1'};
String cardone="A9 75 67 B3";
String cardtwo="";
String cardthree="";
String cardfour="";
float dianya = 0;     
char panduan;         
char SRmima[6];       
Servo duoji;          
int LCD =17;          
int lv = 15;          
int Fhong = 8;        
int lan = 14;         
int i=0;              
int y=0;              
int op = 0;
String content= "";
char key;
int zhengque=0;       
int cuowu=0;          
int dengdai = 30;     
int MMweishu = 6;     
int v=5;              
int z = 0;            
int GMzhengque=0;     
int GMweishu =  6;    
int jiaodu = 0;       
Keypad mykeypad = Keypad (makeKeymap(hexaKeys),hangjiao,liejiao, hang, lie); 

void print_lcd_text(char* text1, char* text2) {
	lcd.clear();
    lcd.setCursor(0, 0);
    lcd.print(text1);
    lcd.setCursor(0, 1);
    lcd.print(text2);
}
void didi(int vv) {
	if(vv==1){
	  digitalWrite(Fhong,HIGH); 
      delay(1000) ; 
      digitalWrite(Fhong,LOW);			
	}
	else if(vv==2){
	  for (int qq = 0;  qq<3;qq++){
	    digitalWrite(Fhong,HIGH);
        delay(200);
        digitalWrite(Fhong,LOW);  
	    delay(200);
  }	
	}
	  else if(vv==3){
	  digitalWrite(Fhong,HIGH); 
      delay(100) ; 
      digitalWrite(Fhong,LOW);			
	}
}
void one (void){
    lcd.clear();
    lcd.setCursor(0,0);
    lcd.print ("New Card Success");
    lcd.setCursor(0,1);
    lcd.print ("      OK!!");
	didi(1);
    delay(3000);
}
void zero (void){
    op= 0 ;
    i = 0;                              
    cuowu = 0;                          
    zhengque = 0;                         
    GMzhengque = 0;                       
    z = 0; 
    y=0;
	key = NO_KEY;
}





void RC522 (void){
  if ( ! mfrc522.PICC_IsNewCardPresent()){
    return;                                   
  }
  if ( ! mfrc522.PICC_ReadCardSerial()) {
    return;
  }                            
  String content= "";       
  byte letter;              
  for (byte i = 0; i < mfrc522.uid.size;i++){
    content.concat (String(mfrc522.uid.uidByte[i] < 0x10 ? " 0": " "));    
    content.concat (String(mfrc522.uid.uidByte[i],HEX));
  }
  content.toUpperCase();         
 
  if (content.substring(1) == cardone || content.substring(1) == cardtwo|| content.substring(1) == cardthree|| content.substring(1) == cardfour){ 

    lcd.clear();                     
    lcd.setCursor(0,0);              
    lcd.print("ID : ");              
    lcd.print (content.substring(1));
    lcd.setCursor(0,1);              
    lcd.print("       YES        "); 
    digitalWrite(lv, HIGH);          
    digitalWrite(Fhong, HIGH);       
    delay(500);                      
    digitalWrite(Fhong, LOW);        
    for(jiaodu = 70; jiaodu < 180; jiaodu++){  
      duoji.write(jiaodu);                  
      delay(15);                            
    }
    delay(1000);    
    for(jiaodu = 180; jiaodu>=70; jiaodu--){     
      duoji.write(jiaodu);                  
      delay(15);                            
    } 
    i = 0;                           
    cuowu = 0;                       
    zhengque = 0;                    
    GMzhengque = 0;                  
    z = 0;                           
    digitalWrite(lv, LOW);           
    char key = mykeypad.getKey();     
    key = NO_KEY;                    
}


  else{
    lcd.clear();                      
    lcd.setCursor(0, 0);              
    lcd.print("ID : ");               
    lcd.print (content.substring(1)); 
    lcd.setCursor(0,1);               
    lcd.print ("       NO        ") ; 
    digitalWrite(Fhong, HIGH);
    delay(300);               
    digitalWrite(Fhong, LOW); 
    delay(300);               
    digitalWrite(Fhong, HIGH);
    delay(300);               
    digitalWrite(Fhong, LOW); 
    delay(300);               
    digitalWrite(Fhong, HIGH);
    delay(300);               
    digitalWrite(Fhong, LOW); 
    char key = mykeypad.getKey();  
    i = 0;                     
    cuowu = 0;                 
    zhengque = 0;              
    GMzhengque = 0;            
    z = 0;                     
    key = NO_KEY;              
  }
}

void setup() {
  duoji.write(70);   
  digitalWrite(Fhong, LOW); 
  SPI.begin();              
  mfrc522.PCD_Init();       
  duoji.attach(16);         
  pinMode(lv, OUTPUT);      
  pinMode(LCD, OUTPUT);     
  pinMode(Fhong, OUTPUT);   
  pinMode(lan, OUTPUT);     
  digitalWrite(LCD, HIGH);  
  lcd.init();               
  lcd.backlight();          
  print_lcd_text("In Opening", "................");
  digitalWrite(lan, HIGH);       
  digitalWrite(lv, HIGH);        
  delay(3000);                   
  lcd.clear();           
  digitalWrite(LCD, LOW);        
  digitalWrite(lv, LOW);         
  digitalWrite(Fhong, LOW);      
}
void loop() {
  dianya = analogRead(7)*5.0/1023; 
  delay(100);     
  if(dianya>3){
  start:
  time_now = millis();
  digitalWrite(LCD,HIGH);  
  lcd.init(); 
  lcd.backlight();
  print_lcd_text("*=Input Password", "#=Pay By Card");
  while(1){
    if(millis() > time_now + 10000){   
      lcd.clear();
      digitalWrite(LCD, LOW);  
      op=0;
      zhengque=0;
      i =0;
      z = 0;
      return;
    }
    delay(1);
    char key = mykeypad.getKey();  
    if(key != NO_KEY){
      panduan = key;
      if (panduan=='*'){
        didi(3);   
        op=0;
        zhengque=0;
        i =0;
        z = 0;
		print_lcd_text("Enter password", "");
        while (1){
          char key = mykeypad.getKey();  
          if(key != NO_KEY &&i<MMweishu){   
            SRmima[i]=key;                  
            lcd.setCursor(0,0);
            lcd.print("Enter password");
            lcd.setCursor(op,1);
            lcd.blink();
            lcd.print(key); 
            didi(3);
            op++;
            if(SRmima[i]==mima[i] ){          
              zhengque++;                     
            }
            i++;
            if(SRmima[z]==gaimi[z] ){ 
              SRmima[z]=key;                  
              delay(100);
              GMzhengque++;                    
            }
            z++;          
            if(SRmima[y]==changcard[y] ){          
              SRmima[y]=key;                   
              delay(100);                      
              GMzhengque++;                    
            }
            y++;                             
          }
          if (GMzhengque == GMweishu && y == GMweishu){ 
            zero();			
			print_lcd_text("*=Add New Card", "#=del Old Card");
            while (1){
              char key = mykeypad.getKey();  
                 if(key != NO_KEY){
                   panduan = key;
                   if (panduan=='*'){ 
				     print_lcd_text("Put You New Card", "");
                     didi(3);
                     for (int ss=0;ss<10;ss++){
                       delay(1000);
                       if ( ! mfrc522.PICC_IsNewCardPresent()){}
                       if ( ! mfrc522.PICC_ReadCardSerial()){}
                       String content= "";       
                       byte letter;              
                       for (byte i = 0; i < mfrc522.uid.size;i++){
                         content.concat (String(mfrc522.uid.uidByte[i] < 0x10 ? " 0": " "));    
                         content.concat (String(mfrc522.uid.uidByte[i],HEX));
                       }
                         content.toUpperCase();         
                       if (content.substring(1) != cardone&&content.substring(1) != cardtwo&&content.substring(1) != cardthree&&content.substring(1) != cardfour){
                         if(cardone==""){
                           cardone=content.substring(1);
                           one();
                         }
                         else if(cardtwo==""){
                           cardtwo=content.substring(1); 
                           one();
                         }
                         else if(cardthree==""){
                           cardthree=content.substring(1); 
                           one();
                         } 
                         else if(cardfour==""){
                           cardfour=content.substring(1); 
                           one();
                         }
                         else{
						   print_lcd_text("      Error     ","  Card Is Full  ");
                           didi(2);
                           delay(3000);
                         }
                       digitalWrite(17,LOW);
                       return;   
                       }
                     }
                   }
				   if (panduan=='#'){
                     didi(3);
					 two:  
					 key = NO_KEY;
				     lcd.clear();
				     lcd.setCursor(0, 0);
				     lcd.print("ID1="+cardone);
				     lcd.setCursor(0, 1);
				     lcd.print("*=Next     #=Del");
					 while(1){
					   char key = mykeypad.getKey();  
					   if(key != NO_KEY){
					     panduan = key;
						 if (panduan=='#'){
                           didi(3);
					       cardone=""; 
						   print_lcd_text(" Del Old Card", "    Success");
						   didi(1);
						   delay(2000);
						   key = NO_KEY;
						   goto start;
						 }						   
						 if (panduan=='*'){
                           didi(3);
					       lcd.clear();
				           lcd.setCursor(0, 0);
				           lcd.print("ID2="+cardtwo);
				           lcd.setCursor(0, 1);
				           lcd.print("*=Next     #=Del"); 
						   while(1){
						     char key = mykeypad.getKey();  
					         if(key != NO_KEY){
					           panduan = key;
						         if (panduan=='#'){
                                   didi(3);
					               cardtwo=""; 
						           print_lcd_text(" Del Old Card", "    Success");
                                   didi(1);
						           delay(2000);
						           key = NO_KEY;
						           goto start; 
						         }
								if (panduan=='*'){
                                  didi(3);
					              lcd.clear();
				                  lcd.setCursor(0, 0);
				                  lcd.print("ID3="+cardthree);
				                  lcd.setCursor(0, 1);
				                  lcd.print("*=Next     #=Del"); 
						          while(1){ 
								    char key = mykeypad.getKey();  
					                if(key != NO_KEY){
					                  panduan = key;
						              if (panduan=='#'){
                                        didi(3);
					                    cardthree=""; 
						                print_lcd_text(" Del Old Card", "    Success");
						                didi(1);
						                delay(2000);
						                key = NO_KEY;
						                goto start; 
						              }
									  if (panduan=='*'){
                                        didi(3);
									    lcd.clear();
				                        lcd.setCursor(0, 0);
				                        lcd.print("ID4="+cardfour);
				                        lcd.setCursor(0, 1);
				                        lcd.print("*=Next     #=Del"); 
						                while(1){ char key = mykeypad.getKey();  
					                      if(key != NO_KEY){
					                        panduan = key;
						                    if (panduan=='#'){
                                              didi(3);
					                          cardfour=""; 
						                      print_lcd_text(" Del Old Card", "    Success");
                                              didi(1);
						                      delay(2000);
						                      key = NO_KEY;
						                      goto start; 
						                    }
							                if (panduan=='*'){
                                              didi(3);
					                          goto two;
						                    }
					                      }
						                }
									  }  
					                }
						          }
								}  
					         }
						   }
						 } 
					   }   
					 }
				   }
                 }
               }
             }

          if (GMzhengque == GMweishu && z == GMweishu){ 
            zero();
			print_lcd_text("New password ","");
            while(i<100){
              char key2 = mykeypad.getKey();  
              if(key2 != NO_KEY &&z<GMweishu){   
                     didi(3);
                mima[z]=key2;
                lcd.setCursor(op,1);
                lcd.blink();
                lcd.print(key2);
                op++;
                z++;
                delay(100); 
              }
              if(key2 != NO_KEY &&z==GMweishu){
                break;
              }
            }
            lcd.clear();
            lcd.setCursor(0,0);
            lcd.print("New password OK!!");
			for (int ww = 0;  ww<=5;ww++){
              lcd.setCursor(ww,1);
              lcd.print(mima[ww]);
			}
            delay(5000);
            lcd.clear();
            digitalWrite(LCD, LOW);  
            zero();
            return;
          }

          if (zhengque == MMweishu && i == MMweishu){     

            print_lcd_text("Opening door","..............");
            digitalWrite(lv, HIGH);   
            digitalWrite(Fhong, HIGH);   
            delay(500); 
            digitalWrite(Fhong, LOW);   
            for(jiaodu = 70; jiaodu < 180; jiaodu++){  
              duoji.write(jiaodu);                  
              delay(15);                            
            }
            delay(1000);    
            for(jiaodu = 180; jiaodu>=70; jiaodu--){     
              duoji.write(jiaodu);                  
              delay(15);                            
            } 
            zero();
            digitalWrite(lv, LOW);   
            delay(5000);
            digitalWrite(LCD, LOW);  
            return;
          }

          if (zhengque != MMweishu && i == MMweishu && GMzhengque != GMweishu){  

            cuowu++;  
            i = 0;
			y=0;
            op = 0;                     
            zhengque = 0;              
            GMzhengque=0;
            z=0;
            lcd.clear();
            lcd.setCursor(0,0);
            lcd.print("wrong  "+String(cuowu)+"  time!");
                             didi(2);
            char key = mykeypad.getKey(); 
            lcd.clear();
            lcd.setCursor(0,0);
            lcd.print("Enter password");
            lcd.setCursor(0,1);
            lcd.blink();

}

          if (cuowu==3){             

            for (int z = dengdai;  z>=0;z--){                   
              lcd.clear();
              lcd.setCursor(0,0);
              lcd.print("Please wait 30 S !");
              lcd.setCursor(0,1);
              lcd.print(z);
              delay(1000);        
            }
            op = 0;  
            cuowu = 0;   
            lcd.clear();
            digitalWrite(LCD, LOW);
            return;

            }
        }
      }
          if (panduan=='#'){
                     didi(3);
            op=0;
            zhengque=0;
            i =0;
            z = 0;
			print_lcd_text("Please swipe card!","");
            for (int a=0 ;a<10;a++){
              delay(1000);
              RC522 () ;
            }
          }
      }
    }
  }
  char key = mykeypad.getKey();  
  if(key != NO_KEY){             
  goto start;
  }
}

