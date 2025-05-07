/*
 Navicat Premium Dump SQL

 Source Server         : Lokal laptop
 Source Server Type    : MySQL
 Source Server Version : 50173 (5.1.73-community)
 Source Host           : localhost:3306
 Source Schema         : fdssb

 Target Server Type    : MySQL
 Target Server Version : 50173 (5.1.73-community)
 File Encoding         : 65001

 Date: 19/01/2025 06:47:02
*/

SET NAMES utf8;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for m_interlink
-- ----------------------------
DROP TABLE IF EXISTS `m_interlink`;
CREATE TABLE `m_interlink`  (
  `id` bigint(20) NOT NULL,
  `link_type` int(11) NULL DEFAULT NULL,
  `echo_period` bigint(20) NULL DEFAULT NULL,
  `monitor_period` bigint(20) NULL DEFAULT NULL,
  `timeout_period` bigint(20) NULL DEFAULT NULL,
  `digester_name` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `transporter_name` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `message_type` varchar(15) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `iso_packager` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `message_key_position` int(11) NULL DEFAULT NULL,
  `message_key_length` int(11) NULL DEFAULT NULL,
  `message_key_element` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `description` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of m_interlink
-- ----------------------------

-- ----------------------------
-- Table structure for m_interlink_socket
-- ----------------------------
DROP TABLE IF EXISTS `m_interlink_socket`;
CREATE TABLE `m_interlink_socket`  (
  `id` bigint(20) NOT NULL,
  `m_interlink_id` bigint(20) NOT NULL,
  `connection_type` int(11) NULL DEFAULT NULL,
  `server_address` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `server_port` int(11) NULL DEFAULT NULL,
  `monitor_period` bigint(20) NULL DEFAULT NULL,
  `socket_driver` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `description` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of m_interlink_socket
-- ----------------------------

-- ----------------------------
-- Table structure for m_parameter_group
-- ----------------------------
DROP TABLE IF EXISTS `m_parameter_group`;
CREATE TABLE `m_parameter_group`  (
  `id` bigint(20) NOT NULL,
  `group_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `access_type` int(11) NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of m_parameter_group
-- ----------------------------

-- ----------------------------
-- Table structure for master_pindah
-- ----------------------------
DROP TABLE IF EXISTS `master_pindah`;
CREATE TABLE `master_pindah`  (
  `id` bigint(20) NOT NULL,
  `account_number` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `cif` varchar(8) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `hp_no` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `tin` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `status` varchar(1) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `decrypted_tin` varchar(6) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of master_pindah
-- ----------------------------

-- ----------------------------
-- Table structure for t_daily_sum_transaction
-- ----------------------------
DROP TABLE IF EXISTS `t_daily_sum_transaction`;
CREATE TABLE `t_daily_sum_transaction`  (
  `m_customer_id` bigint(20) NOT NULL,
  `limit_type` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `amount` decimal(30, 5) NULL DEFAULT NULL,
  `amount_mb` decimal(30, 5) NULL DEFAULT NULL,
  `amount_ib` decimal(30, 5) NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of t_daily_sum_transaction
-- ----------------------------

-- ----------------------------
-- Table structure for t_temp
-- ----------------------------
DROP TABLE IF EXISTS `t_temp`;
CREATE TABLE `t_temp`  (
  `field1` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `id` bigint(20) NOT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of t_temp
-- ----------------------------

SET FOREIGN_KEY_CHECKS = 1;
