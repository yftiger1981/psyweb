String.prototype.trim = function() {
	return this.replace(/(^\s*)|(\s*$)/g, "");
}
checkByteLength = function(str, minlen, maxlen) {
	if(str == null)
		return false;
	var l = str.length;
	var blen = 0;
	for( i = 0; i < l; i++) {
		if((str.charCodeAt(i) & 0xff00) != 0) {
			blen++;
		}
		blen++;
	}
	if(blen > maxlen || blen < minlen) {
		return false;
	}
	return true;
}
validate_username = function(str, element) {
	var str = str || element.value;
	str = str.trim();
	if(!checkByteLength(str, 3, 50)) {
		return false;
	}
	var patn = /^([\uFF0E\uFF07\uFF0F\,\.\(\)\'\/\uFF0D\u0391-\uFFE5-]{2,25}|[\uFF0C\uFF0E\uFF07\uFF08\uFF09\uFF0F\,\.\(\)\'\/\uFF0DA-Za-z\s-]{3,50})$/;
	if(patn.test(str)) {
		return true;
	}
	return false;
}
validate_citizenid = function(str, element) {
	var str = str || element.value;
	str = str.trim();
	var patn_18 = new RegExp("^\\d{17}?\\w$");
	var patn_15 = new RegExp("^\\d{15}$");
	if(patn_18.test(str) || patn_15.test(str)) {
		return true;
	} else {
		return false;
	}
}
validate_date = function(str) {
	var datestr = str;

	var lthdatestr;
	if(datestr != "")
		lthdatestr = datestr.length;
	else
		lthdatestr = 0;

	var tmpy = "";
	var tmpm = "";
	var tmpd = "";
	var status;
	status = 0;
	if(lthdatestr == 0) {
		return 1;
	}

	for( i = 0; i < lthdatestr; i++) {
		if(datestr.charAt(i) == '-') {
			status++;
		}
	}
	if(status == 2 && (lthdatestr > 10 || lthdatestr < 8)) {
		return 1;
	}
	if(status == 2) {
		tmparray = datestr.split("-");
		tmpy = tmparray[0];
		tmpm = tmparray[1];
		tmpd = tmparray[2];
	}

	var year = tmpy;
	var month = tmpm;
	var day = tmpd;

	if((tmpy.length != 4) || (tmpm.length > 2) || (tmpd.length > 2) || isNaN(tmpy)) {
		return 1;
	}
	if(!((1 <= month) && (12 >= month) && (31 >= day) && (1 <= day))) {
		return 1;
	}
	if(!((year % 4 == 0 && year % 100 != 0) || year % 400 == 0) && (month == 2) && (day == 29)) {
		return 1;
	}
	if((month == 2) && (day == 30 || day == 31)) {
		return 1;
	}
	if((month <= 7) && ((month % 2) == 0) && (day >= 31)) {
		return 1;
	}
	if((month >= 8) && ((month % 2) == 1) && (day >= 31)) {
		return 1;
	}

	var datDate = new Date(year, month - 1, day);
	//Test the Date
	if(isNaN(datDate))//Error Date Format
		return 1;
	if(datDate.getMonth() != month - 1 || datDate.getDate() != day)//invalid date such as '1999/02/29' and '1999/04/31'
		return 1;
	var minDate = new Date(1900, 0, 1);
	if(datDate < minDate)//invalid date such as '1999/02/29' and '1999/04/31'
		return 1;
	return 0;
}
validate_birthday = function(str, element) {
	var str = str || element.value;
	//����������֤
	var tmp = validate_date(str);
	if(tmp == 1)
		return false;
	//���������֤ͨ�������뵱ǰ���ڱȽϣ��������ڲ����ڵ�ǰ����֮��)
	tmparray = str.split("-");
	year = tmparray[0];
	month = tmparray[1];
	day = tmparray[2];
	var datDate = new Date(year, month - 1, day);
	//����ĳ�������
	var today = new Date();
	//��ǰ����
	if(datDate >= today)
		return false;
	return true;
}
//ͨ�����֤��ȡ������Ϣ����ʽyyyy-mm-dd
getBirthdayFromID = function(citizenid) {
	var birth = '';
	if(citizenid.length == 15) {
		birth = '19' + citizenid.substring(6, 8) + '-' + citizenid.substring(8, 10) + '-' + citizenid.substring(10, 12);
	} else if(citizenid.length == 18) {
		birth = citizenid.substring(6, 10) + '-' + citizenid.substring(10, 12) + '-' + citizenid.substring(12, 14);
	}
	return birth;
}
//ͨ�����֤��ȡ�Ա���Ϣ����ʽM|F
getSexFromID = function(citizenid) {
	var sSex = '';
	if(citizenid.length == 15) {
		sSex = citizenid.substring(14, 15) % 2 ? 'M' : 'F';
	} else if(citizenid.length == 18) {
		sSex = citizenid.substring(16, 17) % 2 ? 'M' : 'F';
	}
	return sSex;
}
//��֤���֤
validate_citizenid = function(str, element) {
	var str = str || element.value;
	str = str.trim();
	var patn_18 = new RegExp("^\\d{17}?\\w$");
	var patn_15 = new RegExp("^\\d{15}$");

	if(patn_18.test(str) || patn_15.test(str)) {
		return true;
	} else {
		return false;
	}
}
//���պ���
validate_passport = function(str, element) {
	var str = str || element.value;
	str = str.trim();
	if(str.length > 50 || str.length < 3) {
		return false;
	}
	return true;
}
//����֤��ʿ��֤����
validate_militaryCert = function(str, element) {
	var str = str || element.value;
	str = str.trim();
	if(!checkByteLength(str, 6, 50)) {
		return false;
	}
	return true;
}
//�۰�̨����֤��̨��֤��
validate_homeReturnCert = function(str, element) {
	var str = str || element.value;
	str = str.trim();
	if(str.length > 50 || str.length < 5) {
		return false;
	}
	return true;
}
//��֤�����֤������֤��
validate_otherCertcode = function(str, element) {
	var str = str || element.value;
	str = str.trim();
	if(str.length > 50 || str.length < 3) {
		return false;
	}
	return true;
}
//��֤�ǳ�
validate_nickname = function(str, element) {
	var str = str || element.value;

	if(str.length < 4) {
		return "�û�����������4λ�������������á�";
	}

	if(str.length > 30) {
		return "�û������ܶ���30λ�������������á�";
	}

	var numpatn = /^[0-9]{12,30}$/;
	if(numpatn.test(str)) {
		return "���û���Ϊ�����֣����Ȳ��ܳ���11λ��";
	}

	var patn = /^[0-9a-zA-Z_-]+$/;
	if(!patn.test(str)) {
		return "�û�������Ϊ��ĸ�����֡���-������_��������ַ��������������á�";
	}

	var idcpatn = /(^\d{15}([0-9])$)|(^\d{17}([0-9]|x|X)$)/;
	if(str.length < 19 & idcpatn.test(str)) {
		return "�������������֤�������Ƶ��ַ�����Ϊһ��ͨ�û�����";
	}

	if(str.substr(0, 1) == "_" || str.substr(0, 1) == "-") {
		return "��_����-����������λ�������������á�";
	}
	return true;
}
//��֤����
validate_email = function(str, element) {
	var str = str || element.value;
	var patn = /^[_a-zA-Z0-9\-]+(\.[_a-zA-Z0-9\-]*)*@[a-zA-Z0-9\-]+([\.][a-zA-Z0-9\-]+)+$/;
	if(patn.test(str) && str.length > 5) {
		return true;
	} else {
		return false;
		//incorrect format
	}
}
//��֤�ֻ�
validate_mobile = function(str, element) {
	var str = str || element.value;
	var patn = /^[1][34578][0-9]{9}$/;
	if(patn.test(str))
		return true;
	return false;
}
//��֤У����
validate_checkcode = function(str, element) {
	var str = str || element.value;
	var patn = /^[0-9]{4}$/;
	if(patn.test(str))
		return true;
	return false;
}
//��֤����
validate_password = function(str, element) {
	var str = str || element.value;
	var patn = /^[a-zA-Z0-9]{8,16}$/;
	var patn1 = /^[0-9]{8,16}$/;
	var patn2 = /^[a-zA-Z]{8,16}$/;
	if(patn1.test(str) || patn2.test(str))
		return false;
	if(patn.test(str))
		return true;
	return false;
}
//��֤�ֻ���̬��
validate_activeno = function(str, element) {
	var str = str || element.value;
	var patn = /^[0-9]{6}$/;
	if(patn.test(str))
		return true;
	return false;
}
isToaCard = function(cardNo) {
	var isFlag = false;
	var toa_card_bin = "627069|627068|627067|627066";

	if(cardNo == "") {
		return false;
	}
	var patn = /^[0-9]{16}$/;
	if(patn.test(cardNo)) {
		var arr = toa_card_bin.split("|");
		for(var i = 0; i < arr.length; i++) {
			if(cardNo.substring(0, 6) == arr[i]) {
				isFlag = true;
				break;
			}
		}
	}
	return isFlag;
}
isJiejiCard = function(cardNo) {
	var isFlag = false;
	var toa_card_bin = "602907|622298|622986|622989|622155|622156|622157" + "|526855|528020|531659|356869|627066|627067|627068|627069";

	if(cardNo == "") {
		return false;
	}
	var patn = /^[0-9]{16}$/;
	if(patn.test(cardNo)) {
		var arr = toa_card_bin.split("|");
		for(var i = 0; i < arr.length; i++) {
			if(cardNo.substring(0, 6) == arr[i]) {
				isFlag = true;
				break;
			}
		}
	}
	return isFlag;
}
//��궨λ����һ�������ʾ������Ϣ��ֻ֧��ҳ��У�飩
moveFocus2FirstErrorInput = function(array) {
	for(var i = 0; i < array.length; i++) {
		var id = array[i];
		var obj = document.getElementById(id);
		if(obj != null && obj.type != 'radio' && $('#' + id).validator('check') == false) {
			try {
				obj.focus();
				setTimeout("$('#" + id + "').validator('check');$('#" + id + "').validator('showMessage');", 100);
			} catch(e) {
			}
			break;
		} else if(obj == null || obj.type == 'radio') {
			var radioObj = document.getElementsByName(id);
			if(radioObj != null) {
				var isSelect = false;
				for(var j = 0; j < radioObj.length; j++) {
					if(radioObj[j].type == 'radio' && radioObj[j].checked == true) {
						isSelect = true;
						break;
					}

				}
				if(isSelect == false) {
					try {
						radioObj[0].focus();
					} catch(e) {
					}
					break;
				}

			}
		}

	}
}
fivekey_validate_username = function(str, element) {
	var str = str || element.value;
	str = str.trim();
	if(!checkByteLength(str, 3, 50)) {
		return false;
	}
	var patn = /^([\uFF0E\uFF07\uFF0F\,\.\(\)\'\/\uFF0D\u4E00-\u9FA5\uF900-\uFA2D-]{2,25}|[\uFF0C\uFF0E\uFF07\uFF08\uFF09\uFF0F\,\.\(\)\'\/\uFF0DA-Za-z\s-]{3,50})$/;
	if(patn.test(str)) {
		return true;
	}
	return false;
}
//��֤�����ţ�
validate_policyNo = function(str, element) {
	var str = str || element.value;
	var policyReg = new RegExp("^[0-9a-zA-Z]{16,20}$");
	//var policyReg = new RegExp("^/W/D{16,20}$");
	if(policyReg.test(str)) {
		return true;
	}
	return false;
}
//��֤���񿨺ţ�
validate_kkcard = function(str, element) {
	var str = str || element.value;
	str = str.replace(/[ ]/g, "");
	var kkcardReg = new RegExp("^[0-9]{16}$");
	if(kkcardReg.test(str)) {
		return true;
	}
	return false;
}
//��֤qq�ţ�
validate_QQ = function(str, element) {
	var str = str || element.value;
	var kkcardReg = new RegExp("^[0-9]{5,20}$");
	if(kkcardReg.test(str)) {
		return true;
	}
	return false;
}